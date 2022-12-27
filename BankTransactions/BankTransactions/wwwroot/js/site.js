// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const tableRow = document.getElementsByClassName("table-row")
for (let element of tableRow) {
    element.addEventListener("click", (event) => {
        console.log(event.target.tagName == "BUTTON")
        if (event.target.tagName == "BUTTON") {
            console.log('AQUI')
            event.stopPropagation()
            return
        } else {

            location.assign(`Transaction/AddOrEdit/${element.id}`)
        }
    })
}








