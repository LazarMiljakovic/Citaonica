import { Citaonica } from "./Citaonica.js";
import { Grad } from "./Grad.js";
 
let listagradova = [];

let logo = document.createElement("div");
logo.className = "logo";
document.body.appendChild(logo);



let Heder1 = document.createElement("label");
Heder1.className = "Heder1";
Heder1.innerHTML = "ONLINE";
logo.appendChild(Heder1);

let Hedera = document.createElement("label");
Hedera.className = "Hedera";
Hedera.innerHTML = "CITAONICA";
logo.appendChild(Hedera);


fetch("https://localhost:5001/Grad/PreuzmiGradove")
.then(p=>{
    p.json().then(gradovi =>{
        gradovi.forEach(grad =>{
            var f = new Grad(grad.id , grad.naziv);
            listagradova.push(f);
        })
        var f = new Citaonica(listagradova);
        f.crtaj(document.body);
    })

})


