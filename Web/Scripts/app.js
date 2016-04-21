;
var MyModule = (function (window, undefined) {

    function myMethod() {
        alert('my method');
    }

    function myOtherMethod() {
        alert('my other method');
    }

    function AppViewModel() {
        this.firstName = "Bert";
        this.lastName = "Bertington";

        ko.applyBindings(new AppViewModel());
    }

    // explicitly return public methods when this object is instantiated
    return {
        someMethod: myMethod,
        someOtherMethod: myOtherMethod,
        knockMethod : AppViewModel
    };
    
    // Activates knockout.js
    ko.applyBindings(new AppViewModel());

})(window);


