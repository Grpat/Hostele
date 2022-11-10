function initializeInactivityTimer() {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeyup= resetTimer;
    
    function resetTimer() {
        clearInterval(timer);
        let counter=0;
        timer = setInterval(function(){
            counter++;
            const data = { count: counter};
            $.ajax({
                type: "POST",
                url: "/Home/Logout",
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (response) {
                    console.log(counter);
                    window.location.href = response.redirectToUrl;
                }
            });
        }, 60000); 
    }
}
initializeInactivityTimer();
