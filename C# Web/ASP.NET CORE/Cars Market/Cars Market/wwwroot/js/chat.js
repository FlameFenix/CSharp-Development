"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    if (user === "" ||
        user.length == 0 ||
        message === "" ||
        message.length == 0) {
        return;
    }

    var divListGroup = document.createElement("div");
    divListGroup.classList.add("list-group-item", "list-group-item-action");
    var divElement = document.createElement("div");
    divListGroup.classList.add("d-flex", "w-100","justify-content-between");
    var h5Element = document.createElement("h5");
    h5Element.textContent = `${user} says:`;
    h5Element.classList.add("mb-1");
    var smallElement = document.createElement("small");
    const dtNow = new Date();
    smallElement.textContent = `${ message }`;
    divElement.appendChild(h5Element);
    divElement.appendChild(smallElement);
    divListGroup.appendChild(divElement);
    var pElement = document.createElement("p");
    pElement.textContent = dtNow.toLocaleString();
    divListGroup.appendChild(pElement);
    var listGroup = document.getElementById("messageList");
    listGroup.appendChild(divListGroup);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});