(function () {
    'use strict'


    var url = require('url');
    var gitHubHelper = require('./gitHelp.js');
    var gutil = require('gulp-util');

    var repo = 'E2E_PullRequests';
    var sourceBranch = 'successStatus';
    var targetBranch = 'master';
    var updateFilePath = 'prtesting/prtest.md';
    var passState = 'success';
    var errorState = 'failure';
    var testAccount = 'OPSE2ETesting@outlook.com';
    var testPassword = 'qwe123##';
    var gitUser = 'OPSTest';
    var gitToken = 'a40bcb4324d7312a036501061a42d24ab2a62e32';



    describe('[Build/Publish] Pull Request E2E Test]', function () {
        afterAll(function (done) {
            process.nextTick(done)
        });


        it('User create pull request to merge changes to master/live branch', function () {

            var flow = browser.controlFlow();
            var date = new Date();
            var localeTime = date.toLocaleString();
            var commitMessage = 'update e2e test file at ' + localeTime;
            var content = new Buffer('#' + localeTime).toString('base64');

            flow.execute(function () {
                // call github api to generate a git commit operation.
                return gitHubHelper.updateFile(repo, sourceBranch, updateFilePath, commitMessage, content, gitUser, gitToken);
            });

            flow.execute(function () {
                // call github api to generate and merge a git pull request operation.
                return gitHubHelper.merge(repo, gitUser, gitToken, sourceBranch, targetBranch, passState);
            });

            // requires.user.OpenPortalPage();

            // flow.execute(function() {
            //     // call github api to generate and merge a git pull request operation.
            //     return gitHubHelper.tryme(repo, gitUser, gitToken, sourceBranch, targetBranch, passState);
            // });


            // flow.execute(function(){
            //     return  gitHubHelper.validatePRStatus(gitUser, repo, sourceBranch, gitToken, passState);
            // });

            // flow.execute(function() {
            //     // call github api to generate and merge a git pull request operation.
            //     return requires.gitHubHelper.mergePullRequest(config.repo, browser.params.gitUser, browser.params.gitToken, config.sourceBranch, config.targetBranch);
            // });
        });
    });

})();
