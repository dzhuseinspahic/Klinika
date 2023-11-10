function showPacijent(brKnjizice) {
    //for Edit
    /*if (document.getElementById("parPacijent") != null) {
        document.getElementById("parPacijent").style.display = "none";
    }*/

    if (document.getElementById("parErrorPacijent") != null) {
        document.getElementById("parErrorPacijent").style.display = "none";
    }

    if (brKnjizice.length != 8) {
        document.getElementById("txtPacijent").style.display = "none";
        document.getElementById("inputPacijentID").value = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var response = this.responseText.split(" ID: ");
                document.getElementById("txtPacijent").style.display = "inline";
                document.getElementById("txtPacijent").innerHTML = "Ime i prezime: " + response[0];
                document.getElementById("inputPacijentID").value = response[1];
            }
        };
        var param = 'param=' + brKnjizice;
        xmlhttp.open("GET", "/Prijem/FindPacijentByBrKnjizice?" + param, true);
        xmlhttp.send();
    }
}

function showLjekar(sifra) {
    //for Edit
    /*if (document.getElementById("parLjekar") != null) {
        document.getElementById("parLjekar").style.display = "none";
    }*/

    if (document.getElementById("parErrorLjekar") != null) {
        document.getElementById("parErrorLjekar").style.display = "none";
    }

    if (sifra.length != 8) {
        document.getElementById("txtLjekar").style.display = "none";
        document.getElementById("inputLjekarID").value = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var response = this.responseText.split(" sifra: ");
                document.getElementById("txtLjekar").style.display = "inline";
                document.getElementById("txtLjekar").innerHTML = "Ime i prezime: " + response[0];
                document.getElementById("inputLjekarID").value = response[1];
            }
        };
        var param = 'param=' + sifra;
        xmlhttp.open("GET", "/Prijem/FindLjekarBySifra?" + param, true);
        xmlhttp.send();
    }
}