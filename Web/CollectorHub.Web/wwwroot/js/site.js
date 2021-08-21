// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




function reloadIFrame() {

    var wordMeterBtnId = "tstBtn"; // type speed test
    var wordMeterIframeId = "tst"; // type speed test
    var link = 'https://typing-speed-test.aoeu.eu/?iframe=1;lang=en';

    var refreshBtn = document.getElementById(wordMeterBtnId);
    refreshBtn.addEventListener('click', () => {

        var iframe = document.getElementById(wordMeterIframeId).src = link;
    });

}













