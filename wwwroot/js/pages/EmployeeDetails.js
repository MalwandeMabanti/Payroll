function onDataErrorOccured(data) {
    let errors = JSON.parse(data.error.message);
    let errorMessages = [];
    Object.keys(errors).forEach(key => {
        errorMessages.push(...[errors[key]]);
    });
    setTimeout(() => {
        document.querySelector(".dx-error-message").innerHTML = "";
        errorMessages.forEach(message => {
            document.querySelector(".dx-error-message").innerHTML += message.toString().replace(/'/g, "") + "<br />";
        });
    });
}
//# sourceMappingURL=EmployeeDetails.js.map