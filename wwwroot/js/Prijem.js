function showPacijent(brKnjizice) {
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

async function FilterPrijems() {
    const startDate = document.getElementById("startDate").value;
    const endDate = document.getElementById("endDate").value;
    try {
        var param = 'startDate=' + startDate + '&endDate=' + endDate;
        const response = await fetch('/Prijem/FilterPrijemsByDate?' + param);
        if (!response.ok) {
            throw new Error('HTTP error! Status: ${response.status}');
        }
        const data = await response.text();
        const prijemContainer = document.getElementById("prijemTableContainer");
        prijemContainer.innerHTML = data;
    } catch (error) {
        console.error("Error: " + error.message);
    }
}
