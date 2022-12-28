// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const tableRow = document.getElementsByClassName("table-row")
for (let element of tableRow) {
    element.addEventListener("click", (event) => {
        if (event.target.tagName == "BUTTON" || event.target.nodeName == "I") {
            event.stopPropagation()
            return
        } else {

            location.assign(`Transaction/AddOrEdit/${element.id}`)
        }
    })
}

const tableContent = document.getElementById("table-content")
const filterInput = document.getElementById("filter-input")
const searchBtn = document.getElementById("search")
searchBtn.addEventListener("click", async () => {
    if (filterInput.value) {
        const response = await axios.get("/Transaction/Filter", { params: { name: filterInput.value } })
        tableContent.innerHTML = null
        console.log(response)
        if (response?.data?.transactions?.value.value.transactions.length > 0) {
            response.data.transactions.value.value.transactions.forEach((transaction) => {
                let dateFormated = new Date(transaction.date)
                let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(dateFormated);
                let mo = new Intl.DateTimeFormat('en', { month: 'short' }).format(dateFormated);
                let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(dateFormated);
                dateFormated = `${da}-${mo}-${ye}`
                let g = tableContent.appendChild(document.createElement("tr"))
                g.setAttribute("id", `${transaction.transactionId}`)
                g.setAttribute("class", "table-row")
                g.innerHTML = 
                    `
                    <td> ${transaction.accountNumber}</td >
                    <td> ${transaction.beneficiaryName}</td >
                    <td> ${dateFormated}</td >
                    <td> R$:${transaction.amount},00</td >   
                    <td>
                        <button  data-identification="${transaction.transactionId}" class="btn btn-sm btn-danger mx-auto d-block del-button">
                            <i class="far fa-trash-alt"></i>
                        </button>
                    </td>
                    `
            })
            const delBtns = document.getElementsByClassName("del-button")
            const tableRow = document.getElementsByClassName("table-row")
            for (let element of tableRow) {
                element.addEventListener("click", (event) => {
                    if (event.target.tagName == "BUTTON" || event.target.nodeName == "I") {
                        event.stopPropagation()
                        return
                    } else {

                        location.assign(`Transaction/AddOrEdit/${element.id}`)
                    }
                })
            }

            
            for (let btn of delBtns) {
                btn.addEventListener("click",  async() => {
                    const response = await axios.post(`Transaction/Delete/${btn.attributes["data-identification"].value}`)
                    if (response.status === 200) {
                        alert("Transação deletada com sucesso")
                        location.reload()
                    } else {
                        alert("Ops, não foi possível deletar o transação")
                    }
                })
            }
        }
    } else {
         location.reload()
    }
})













