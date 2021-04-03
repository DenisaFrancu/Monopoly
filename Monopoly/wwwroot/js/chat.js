"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

//Disable send button until connection is established  
// if(document.getElementById("sendBtn")!=null)
//     document.getElementById("sendBtn").disabled = true;
// if(document.getElementById("sendToGroupBtn")!=null)
//     document.getElementById("sendToGroupBtn").disabled = true;

// connection.on("ReceiveMessage", function (user, message, date) {
//     var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
//     var encodedMsg = "[" + date + "] - " + user + ": " + msg;
//     var li = document.createElement("li");
//     li.textContent = encodedMsg;
//     document.getElementById("ulmessages").appendChild(li);
//     updateScroll();
// });

// connection.on("ReceiveGroupMessage", function (user, message) {
//     var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
//     var encodedMsg = user + ": " + msg;
//     var li = document.createElement("li");
//     li.textContent = encodedMsg;
//     console.log("funct", msg);
//     document.getElementById("ulGroupMessages").appendChild(li);
//     updateScroll();
// });

// connection.start().then(function () {
//     // document.getElementById("sendBtn").disabled = false;
// }).catch(function (err) {
//     return console.error(err.toString());
// });
// if(document.getElementById("sendBtn")!=null)
//     document.getElementById("sendBtn").addEventListener("click", function (event) {
//         var message = document.getElementById("txtmessage").value;
//         document.getElementById("txtmessage").value = '';
//         document.getElementById("sendBtn").disabled = true;
//         connection.invoke("SendMessage", message).catch(function (err) {
//             return console.error(err.toString());
//         });    
//         event.preventDefault();
//     });

// document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
//     var message = document.getElementById("txtmessage").value;
//     console.log("document.",message);
//     document.getElementById("txtmessage").value = '';
//     document.getElementById("sendToGroupBtn").disabled = true;
//     connection.invoke("SendMessageToGroup", message, "Private").catch(function (err) {
//         return console.error(err.toString());
//     });    
//     event.preventDefault();
// });

// var input = document.getElementById("txtmessage");

// input.addEventListener("keyup", function(event) {
//   if (event.key  === 'Enter') {
//     event.preventDefault();
//     document.getElementById("sendBtn").click();
//   }
// });

// function updateScroll(){
//     var element = document.getElementById("messagesList");
//     element.scrollTop = element.scrollHeight;
// }

if(document.getElementById("sendBtn")!=null)
{
    document.getElementById("sendBtn").disabled = true;
    connection.on("ReceiveMessage", function (user, message, date) {
        console.log("IN functie");
        var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
        var encodedMsg = "[" + date + "] - " + user + ": " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("ulmessages").appendChild(li);
        updateScrollMainPage();
    });
    connection.start().then(function () {
        // document.getElementById("sendBtn").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendBtn").addEventListener("click", function (event) {
        var message = document.getElementById("txtmessage").value;
        document.getElementById("txtmessage").value = '';
        document.getElementById("sendBtn").disabled = true;
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });    
        event.preventDefault();
    });
    var input = document.getElementById("txtmessage");

    input.addEventListener("keyup", function(event) {
    if (event.key  === 'Enter') {
        event.preventDefault();
        document.getElementById("sendBtn").click();
    }
    });

    function updateScrollMainPage(){
        var element = document.getElementById("messagesList");
        element.scrollTop = element.scrollHeight;
    }
}

if(document.getElementById("sendToGroupBtn")!=null)
{
    
    document.getElementById("sendToGroupBtn").disabled = true;
    connection.on("ReceiveGroupMessage", function (user, message) {
        console.log("IN functie");
        var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
        var encodedMsg = user + ": " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        console.log("funct", msg);
        document.getElementById("ulGroupMessages").appendChild(li);
        updateScroll();
    });
    connection.start().then(function () {
        connection.invoke('AddToGroup', "Private");
    }).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
        var message = document.getElementById("txtmessage").value;
        console.log("document.",message);
        document.getElementById("txtmessage").value = '';
        document.getElementById("sendToGroupBtn").disabled = true;
        connection.invoke("SendMessageToGroup", message, "Private").catch(function (err) {
            return console.error(err.toString());
        });    
        event.preventDefault();
    });
    var input = document.getElementById("txtmessage");
    input.addEventListener("keyup", function(event) {
    if (event.key  === 'Enter') {
        event.preventDefault();
        document.getElementById("sendToGroupBtn").click();
    }
    });

    function updateScroll(){
        var element = document.getElementById("messagesList");
        element.scrollTop = element.scrollHeight;
    }
}