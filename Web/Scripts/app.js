var Module = Module || {};


window.onload = (function () {

    Module.PatientModel = function () {
        this.Patient_Name = ko.observable();
        this.Patient_Address = ko.observable();
    };

    Module.PatientViewModel = function () {
        var patient = ko.observable(),
        loadPatient = function () {
            var newModel = new Module.PatientModel();
            newModel.Patient_Name("John");
            patient(newModel);
        };
        return {
            patient: patient,
            loadPatient: loadPatient
        };
    }();

    Module.PatientViewModel.loadPatient();

    ko.applyBindings(Module.PatientViewModel);


});