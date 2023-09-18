var Qualifications;
(function (Qualifications) {
})(Qualifications || (Qualifications = {}));
function disableRelevantFields(e) {
    if (e.dataField == "EmpId" && e.parentType == "dataRow") {
        e.editorOptions.disabled = !e.row.isNewRow;
    }
    if (e.dataField != "EmpId" && e.parentType == "dataRow") {
        e.editorOptions.disabled = (typeof e.row.data.EmpId !== "number" && Qualifications.companyUser);
    }
}
function setCellValueQualifications(newData, value) {
    this.defaultSetCellValue(newData, value);
}
//# sourceMappingURL=EmployeeQualifications.js.map