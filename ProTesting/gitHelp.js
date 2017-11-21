(function () {
    'use strict';

    var request = require('request');
    var Q = require('q');
    var dateFormat = require('dateformat');
    var URITemplate = require('urijs/src/URITemplate');
    var gutil = require('gulp-util');
    var https = require('https');

    var dateFormatTemplate = 'mm/dd/yyyy HH:MM';

    var createPullRequest = function (repo, gitUser, gitToken, sourceBranch, targetBranch) {
        var deferred = Q.defer();

        // github v3 api for create a pull request.
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/pulls');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo
        });
        var prTitle = dateFormat(new Date(), dateFormatTemplate) + ' : Create Pull Request for branch ' + sourceBranch + ' -> ' + targetBranch;

        var options = {
            baseUrl: 'https://api.github.com/',
            method: 'POST',
            url: path,
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json',
                'Content-Type': 'application/json'
            },
            json: true,
            body: {
                title: prTitle,
                head: sourceBranch,
                base: targetBranch
            }
        };

        request(options, function (error, response, body) {
            if (!error && /^2/.test('' + response.statusCode)) {
                gutil.log('New pull request ' + body.number + ' created.');
                deferred.resolve({
                    number: body.number,
                    hasCommits: true
                });
            } else {
                var reason = {
                    cause: error || (body && body.errors),
                    statusCode: response && response.statusCode,
                    body: body
                };
                if (reason.statusCode === 422 && Array.isArray(reason.cause) && reason.cause.length && /^No commits/.test(reason.cause[0].message)) {
                    gutil.log('No commits between ' + sourceBranch + ' and ' + targetBranch);
                    deferred.resolve({
                        hasCommits: false
                    });
                } else {
                    gutil.log('Create pull request failed, see the reason:');
                    gutil.log(reason);
                    deferred.reject();
                }
            }
        });

        return deferred.promise;
    };

    var mergePullRequest = function (repo, gitUser, gitToken, prNumber, commitMessage) {
        var deferred = Q.defer();

        // github v3 api for merge a pull request.
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/pulls/{number}/merge');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo,
            'number': prNumber
        });

        var options = {
            baseUrl: 'https://api.github.com/',
            method: 'PUT',
            url: path,
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json',
                'Content-Type': 'application/json'
            },
            json: true,
            body: {
                commit_message: commitMessage
            }
        };

        request(options, function (error, response, body) {
            if (!error && /^2/.test('' + response.statusCode)) {
                gutil.log('Merge pull request ' + prNumber + ' finished.');
                deferred.resolve(body);
            } else {
                var reason = {
                    cause: error || (body && body.errors),
                    statusCode: response && response.statusCode,
                    body: body
                };
                gutil.log('Merge pull request failed, see the reason:');
                gutil.log(reason);
                deferred.reject();
            }
        });

        return deferred.promise;
    };

    var merge = function (repo, gitUser, gitToken, sourceBranch, targetBranch, state) {
        var deferred = Q.defer();

        createPullRequest(repo, gitUser, gitToken, sourceBranch, targetBranch).then(function (data) {
            if (!data.hasCommits) {
                deferred.resolve('No commits between ' + sourceBranch + ' and ' + targetBranch);
            }
        }, function (error) {
            deferred.reject();
        })

        validatePRStatus(gitUser, repo, sourceBranch, gitToken, state).then(function (status) {
            if (status.mystate === 'success') {
                gutil.log('hahhahh');
                var commitMessage = dateFormat(new Date(), dateFormatTemplate) + " : Merge Pull Request for branch " + sourceBranch + " -> " + targetBranch;
                mergePullRequest(repo, gitUser, gitToken, 12548, commitMessage).then(function () {
                    deferred.resolve('Merge succeed!');
                }, function () {
                    deferred.reject();
                });
            } else {
                gutil.log('third step');
                deferred.resolve();
            }
        });

        

        return deferred.promise;
    };

    var merge = function (repo, gitUser, gitToken, sourceBranch, targetBranch, state) {
        var deferred = Q.defer();

        createPullRequest(repo, gitUser, gitToken, sourceBranch, targetBranch).then(function (data) {
            if (!data.hasCommits) {
                deferred.resolve('No commits between ' + sourceBranch + ' and ' + targetBranch);
            } else {
                validatePRStatus(gitUser, repo, sourceBranch, gitToken, state).then(function (status) {
                    if (status.mystate === 'success') {
                        var commitMessage = dateFormat(new Date(), dateFormatTemplate) + " : Merge Pull Request for branch " + sourceBranch + " -> " + targetBranch;
                        mergePullRequest(repo, gitUser, gitToken, status.number, commitMessage).then(function () {
                            deferred.resolve('Merge succeed!');
                        }, function () {
                            deferred.reject();
                        });
                    } else {
                        gutil.log('third step');
                        deferred.resolve();
                    }
                });
            }
        }, function (error) {
            deferred.reject();
        });

        return deferred.promise;
    };



    var listPullRequests = function (repo, gitUser, gitToken, sourceBranch, targetBranch, state) {
        var deferred = Q.defer();

        // github v3 api for list pull requests.
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/pulls?state={state}&head={sourceBranch}&base={targetBranch}');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo,
            'state': state,
            'sourceBranch': sourceBranch,
            'targetBranch': targetBranch
        });

        var options = {
            baseUrl: 'https://api.github.com/',
            method: 'GET',
            url: path,
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json'
            },
            json: true
        };

        request(options, function (error, response, body) {
            if (!error && /^2/.test('' + response.statusCode)) {
                var numbers = [];
                for (var i = 0; i < body.length; i++) {
                    numbers.push(body[i].number);
                    gutil.log('Found ' + state + ' pull request: ' + body[i].number);
                }
                deferred.resolve(numbers);
            } else {
                var reason = {
                    cause: error || (body && body.errors),
                    statusCode: response && response.statusCode,
                    body: body
                };
                gutil.log('List Pull Requests failed, see the reason:');
                gutil.log(reason);
                deferred.reject();
            }
        });

        return deferred.promise;
    };

    var cleanPullRequests = function (repo, gitUser, gitToken, sourceBranch, targetBranch) {
        var deferred = Q.defer();

        listPullRequests(repo, gitUser, gitToken, sourceBranch, targetBranch, 'close').then(function (numbers) {
            if (numbers.length > 0) {
                var commitMessage = 'Clean up open Pull Requests by CI job.';
                mergePullRequest(repo, gitUser, gitToken, numbers[0], commitMessage).then(function () {
                    gutil.log('Clean up pull request ' + numbers[0] + ' finished.');
                    deferred.resolve(numbers[0]);
                }, function () {
                    deferred.reject();
                });
            } else {
                gutil.log('No open pull requests to clean.');
                deferred.resolve('No open pull requests');
            }
        }, function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };


    var getSha = function (repo, branch, filepath, gitUser, gitToken) {
        var deferred = Q.defer();

        // github v3 api for get file content.
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/contents/{filepath}?ref={branch}');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo,
            'filepath': filepath,
            'branch': branch
        });

        var options = {
            hostname: 'api.github.com',
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json'
            },
            path: path,
            method: 'GET'
        };

        var request = https.request(options, function (res) {
            var data = "";
            res.setEncoding('utf8');

            res.on('data', function (chunk) {
                data += chunk;
            });

            res.on('end', function () {
                var jsonData = JSON.parse(data);
                deferred.fulfill(jsonData.sha);
            });
        });

        request.on('error', function (e) {
            deferred.reject('get sha failed');
        });

        request.end();

        return deferred.promise;
    };

    var updateFile = function (repo, branch, filepath, commitMessage, content, gitUser, gitToken) {
        var deferred = Q.defer();

        // github v3 api for update a file content.
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/contents/{filepath}');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo,
            'filepath': filepath
        });

        var options = {
            hostname: 'api.github.com',
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json'
            },
            method: 'PUT',
            path: path
        };

        getSha(repo, branch, filepath, gitUser, gitToken).then(function (sha) {
            console.log('sha: ' + sha);

            var parameters = {
                message: commitMessage,
                content: content,
                sha: sha,
                branch: branch
            };

            var jsonParam = JSON.stringify(parameters);
            options.headers['Content-Type'] = 'application/json';
            options.headers['Content-Length'] = jsonParam.length;

            var req = https.request(options, function (res) {
                var data = "";
                res.setEncoding('utf8');

                res.on('data', function (chunk) {
                    data += chunk;
                });

                res.on('end', function () {
                    console.log('update file succeeded.');
                    deferred.fulfill();
                });
            });
            req.write(jsonParam);
            req.on('error', function (e) {
                console.log('update file failed');
                deferred.reject('update file failed');
            });
            req.end();
        });

        return deferred.promise;
    };

    var validatePRStatus = function (gitUser, repo, branch, gitToken, expectedStatus) {
        var deferred = Q.defer();
        var num = 0;
        gutil.log('Waiting ' + num + 5 + ' seconds to validate');
        var pathTemplate = new URITemplate('/repos/{owner}/{repo}/commits/{branch}/status');
        var path = pathTemplate.expand({
            'owner': gitUser,
            'repo': repo,
            'branch': branch,
        });

        var options = {
            baseUrl: 'https://api.github.com/',
            method: 'GET',
            url: path,
            headers: {
                'User-Agent': gitUser,
                'Authorization': 'token ' + gitToken,
                'Accept': 'application/vnd.github.v3.json',
                'Content-Type': 'application/json'
            },
            json: true
        };

        request(options, function (error, response, body) {
            if (error) {
                gutil.log('Validation failed, see the error:')
                gutil.log(error);
                deferred.reject();
            } else {
                if (body.state === 'pending') {
                    setTimeout(function () {
                        validatePRStatus(gitUser, repo, branch, gitToken, expectedStatus)
                    }, 5000);
                    // gutil.log('set time once');
                    // var end = Date.now() + 15 *1000;
                    // while (Date.now() < end){
                    //     deferred.resolve('pending time out');
                    // };

                } else if (body.state === expectedStatus) {
                    gutil.log('Validation success.');
                    deferred.resolve({
                        mystate: body.state,
                        number: 123
                    });
                } else {
                    gutil.log('Validation failed.');
                    gutil.log('Description:' + body.statuses[0].description);
                    gutil.log('Expected status:' + expectedStatus);
                    deferred.reject('Error: wrong status');
                };
            };
        });
        return deferred.promise;
    };

    var tryme = function (repo, gitUser, gitToken, sourceBranch, targetBranch, state) {
        var deferred = Q.defer();
        validatePRStatus(gitUser, repo, sourceBranch, gitToken, state).then(function (status) {
            if (status.mystate === 'success') {
                var commitMessage = dateFormat(new Date(), dateFormatTemplate) + " : Merge Pull Request for branch " + sourceBranch + " -> " + targetBranch;
                mergePullRequest(repo, gitUser, gitToken, 124124, commitMessage).then(function () {
                    deferred.resolve('Merge succeed!');
                }, function () {
                    deferred.reject();
                });
            } else {
                gutil.log('third step');
                deferred.resolve();
            }
        });

        return deferred.promise;
    };

    module.exports = {
        merge: merge,
        updateFile: updateFile,
        mergePullRequest: mergePullRequest,
        createPullRequest: createPullRequest,
        cleanPullRequests: cleanPullRequests,
        validatePRStatus: validatePRStatus,
        tryme: tryme,
        mergesecond: mergesecond,
    };

})();