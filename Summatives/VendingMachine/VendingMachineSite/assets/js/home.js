class Item {
    constructor(element) {
        this.name = element.children()[0]
        this.price = element.children()[1]
        this.quantity = element.children()[2]
        this.id = element.children()[3]
    }
}

var data = new Array()
var change = [0, 0, 0, 0]
var moneyText
var messageText
var changeText
var inputField

// initialize
$(document).ready(Initialize)
function Initialize() {
	function CreateItem(name) {
		let i = new Item($("#"+name))
		data[name] = i
		data.push(i)
	}
	CreateItem("snickers")
	CreateItem("mms")
	CreateItem("almondjoy")
	CreateItem("milkyway")
	CreateItem("payday")
	CreateItem("reeses")
	CreateItem("pringles")
	CreateItem("cheezits")
	CreateItem("doritos")

	moneyText = $("#totalText")
	messageText = $("#messageText")
	changeText = $("#changeText")
	inputField = $("#itemInput")

	$("#purchaseButton").click(Purchase)
	$("#changeButton").click(ReturnChange)

	UpdateQuantities()
}

function UpdateQuantities() {
	$.ajax({
		type: "GET",
		url: `http://localhost:8080/items`,
		success: function(items) {
			for (let i = 0; i < items.length; i++) {
				data[i].quantity.innerText = "Quantity Left: " + items[i].quantity
			}
		},
		error: function() {
			messageText.text("{ Error connecting to API }")
		}
	})
}

function AddMoney(cash) {
	let current = parseFloat(moneyText.text().slice(1))
	moneyText.text(`$${(current + cash).toFixed(2)}`)
}

function Purchase() {
	let id = parseInt(inputField.val())
	if (id == NaN) {
		inputField.val("")
		return
	}

	let selection = data[id - 1]
	if (selection == undefined) {
		inputField.val("")
		return
	}
	
	let cash = parseFloat(moneyText.text().slice(1))
	$.ajax({
		type: "GET",
		url: `http://localhost:8080/money/${cash}/item/${id}`,
		success: function(response) {
			moneyText.text("$0.00")
			change[0] += response.quarters
			change[1] += response.dimes
			change[2] += response.nickels
			change[3] += response.pennies

			let text = "{ "
			if (change[0] > 0)
				text += `${change[0]} Quarter${change[0] > 1 ? "s" : ""} `
			if (change[1] > 0)
				text += `${change[1]} Dime${change[1] > 1 ? "s" : ""} `
			if (change[2] > 0)
				text += `${change[2]} Nickel${change[2] > 1 ? "s" : ""} `
			if (change[3] > 0)
				text += `${change[3]} Pennie${change[3] > 1 ? "s" : ""} `
			text += "}"
			messageText.text("{ Thank You!!! }")
			messageText.attr('style', 'color: green !important')
			changeText.text(text)
			UpdateQuantities()
		},
		error: function(response) {
			if (response.responseJSON != undefined && response.responseJSON.message != undefined)
				messageText.text(`{ ${response.responseJSON.message} }`)
			else
				messageText.text("{ Error connecting to API }")
			messageText.attr('style', 'color: red !important')
		}
	})
}
function ReturnChange() {
	let total = 0
	total += change[0] * 0.25
	total += change[1] * 0.1
	total += change[2] * 0.05
	total += change[3] * 0.01
	AddMoney(total)
	changeText.text("{ }")
	messageText.text("{ }")
	messageText.attr('style', '');
	change = [0, 0, 0, 0]
}