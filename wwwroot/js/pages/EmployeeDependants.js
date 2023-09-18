var Dependants;
(function (Dependants) {
})(Dependants || (Dependants = {}));
function onDataErrorOccurred(e) {
    let errorObject;
    let errorArray = [];
    let errorRow;
    errorObject = JSON.parse(e.error.message);
    Object.keys(errorObject).forEach(key => {
        errorArray.push(...errorObject[key]);
    });
    setTimeout(() => {
        errorRow = document.querySelector('.dx-error-message');
        errorRow.innerHTML = errorArray.join("<br>");
    });
}
function onEditorPreparing(e) {
    if (e.dataField == "EmployeeId" && e.parentType == "dataRow") {
        e.editorOptions.disabled = !e.row.isNewRow;
    }
    if (e.dataField != "EmployeeId" && e.parentType == "dataRow") {
        e.editorOptions.disabled = (typeof e.row.data.EmployeeId !== "number" && Dependants.companyUser);
    }
}
function setCellValue(newData, value) {
    this.defaultSetCellValue(newData, value);
}
//# sourceMappingURL=EmployeeDependants.js.map