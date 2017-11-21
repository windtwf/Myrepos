(function(){
    'use strict';
    
   describe('Protractor demo', function(){
   var firstNumber = element(by.model('first'));
   var secondeNumber = element(by.model('second'));
   var goButton = element(by.id('gobutton'));
   var lastResult = element(by.binding('latest'));
   
   beforeEach(function(){
      browser.get('http://juliemr.github.io/protractor-demo/');
   });
   
   it('should have a title', function() {
       expect(browser.getTitle()).toEqual('Super Calculator');
   });
   
   it('should add one and two',function(){
       firstNumber.sendKeys(1);
       secondeNumber.sendKeys(2);      
       goButton.click();
       
       expect(lastResult.getText()).toEqual('3');      
   });
   
   it('should add four and six', function(){
       firstNumber.sendKeys(4);
       secondeNumber.sendKeys(6);
       goButton.click();
       
       expect(lastResult.getText()).toEqual('10');
   });  
});
})();
