"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
    
document.getElementById("sendToGroupBtn").disabled = true;
connection.on("ReceiveGroupMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("ulGroupMessages").appendChild(li);
    updateScroll();
});
connection.on("JoinedRoomMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + " " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("ulGroupMessages").appendChild(li);
    updateScroll();
});
connection.on("RedirectToGame", function (room) {
    console.log(room)
    document.cookie="Room="+room;
    window.location.href = "Game";
});
connection.start().then(function (){
    connection.invoke('AddToLoby');
    connection.invoke("SendJoinedRoomMessage");
    
    //connection.invoke('AddToLoby').catch(function (err){
    //    console.log(err);
    //    connection.invoke('AddToLoby');
    //});
    //if (!performance.navigation.type == performance.navigation.TYPE_RELOAD){
    //    connection.invoke("SendJoinedRoomMessage").catch(function (err) {
    //        return console.error(err.toString());
    //    });
    //}
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
    var message = document.getElementById("txtmessage").value;
    console.log("document.",message);
    document.getElementById("txtmessage").value = '';
    document.getElementById("sendToGroupBtn").disabled = true;
    connection.invoke("SendMessageToGroup", message).catch(function (err) {
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

document.getElementById("leaveRoom").addEventListener("click", function (event) {
    connection.invoke('RemoveFromGroup');
    event.preventDefault();
});
