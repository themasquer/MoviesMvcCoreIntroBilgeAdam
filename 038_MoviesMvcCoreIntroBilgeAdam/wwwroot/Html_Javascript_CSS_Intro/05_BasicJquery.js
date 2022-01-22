$(document).ready(function () {
    console.log("Script loaded.");
    var inputText;
    $("#sOutput1").text("");
    $("#sOutput2").html("");
    $("#tbOutput1").val("");
    $("#bSetText").click(function () {
        inputText = $("#tbInput1").val();
        $("#sOutput1").text(inputText);
    });
    $("#bSetHTML").click(function () {
        inputText = $("#tbInput1").val();
        $("#sOutput2").html('<label style="color:blue;font-size:24px;"><b>' + inputText + '</b></label>');
    });
    $("#bSetValue").click(function () {
        inputText = $("#tbInput1").val();
        $("#tbOutput1").val(inputText);
    });
});