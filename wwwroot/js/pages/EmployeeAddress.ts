const editors = [];

function employeeAddressOnEditorPreparing(e) {
    if (!e.row.isNewRow) {
        disableEmployeeFieldOnEdit(e);
    } else {
        disableAllExceptEmployeeFieldOnCreate(e);
    }

    if (e.dataField !== "EmployeeId" && e.parentType === "dataRow") {
        e.editorOptions.onInitialized = function (data) {
            editors.push(data.component);
        }
    }

    if (e.dataField === "EmployeeId" && e.parentType === "dataRow") {
        const originalOnValueChanged: Function = e.editorOptions.onValueChanged;
        e.editorOptions.onValueChanged = function (data) {
            originalOnValueChanged(data);

            if (!!e.row.isNewRow) {
                editors.forEach(_ => _.option("disabled", typeof data.value !== "number"));
            }
        }
    }
}

function disableEmployeeFieldOnEdit(e) {
    if (e.dataField === "EmployeeId" && e.parentType === "dataRow") {
        e.editorOptions.disabled = true;
    }
}

function disableAllExceptEmployeeFieldOnCreate(e) {
    if (e.dataField !== "EmployeeId" && e.parentType == "dataRow") {
        e.editorOptions.disabled = true;
    }
}