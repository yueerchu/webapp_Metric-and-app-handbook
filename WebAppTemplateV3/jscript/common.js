function Common_WaitingTimer() {
    var controlID = '';
    var panel = null;

    controlID = 'ctl00_LoadingPanel';
    panel = document.getElementById(controlID);

    panel.className = "Loading_Show";

    setTimeout(function () {
        var Img = document.getElementById('ctl00_LoadingIcon');
        Img.src = "../img/loading_timer.gif";
    })
}
