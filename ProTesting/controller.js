(function(){
    'use strict';
    
    var QueryPage = function(){
        this.searchBox = element(by.id('cli_shellHeaderSearchInput'));
        this.searchButton = element(by.buttonText('Search'));
        this.toc = element(by.id('tocNav'));
        //this.tocpicTitle = element(by.id('api-doc-contents').$('h1'));
        
        this.ask = function(){
            this.searchBox.sendKeys('Hello, Caihua');
            this.searchButton.click();
        }
    };
    
    // if you want to new this function, you should not like following:
    // module.exports = { QueryPage: QueryPage }  
    module.exports = QueryPage;
})();