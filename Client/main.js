import { Citaonica } from "./Citaonica.js";
import { Grad } from "./Grad.js";
 
let listagradova = [];

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


