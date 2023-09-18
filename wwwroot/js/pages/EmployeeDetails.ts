function onDataErrorOccured(data) : void {
    let errors = JSON.parse(data.error.message)
    let errorMessages : string[] = []

    Object.keys(errors).forEach(key => {
        errorMessages.push(...[errors[key]])
    })

    setTimeout(() => {
        document.querySelector(".dx-error-message").innerHTML = "";

        errorMessages.forEach(message => {
            document.querySelector(".dx-error-message").innerHTML += message.toString().replace(/'/g, "") + "<br />"
        })
    })
}