import { Fakultet } from "./Fakultet.js";
import { Grad } from "./Grad.js";

export class Citaonica
{
    constructor(listagradova)
    {
        this.listagradova = listagradova
        this.kont = null;
    }

    crtaj(host)
    {
        this.kont = document.createElement("div");
        this.kont.className= "prvikont";
        host.appendChild(this.kont);


        let kontFormaGradova = document.createElement("div");
        kontFormaGradova.className = "izborGradova";
        this.kont.appendChild(kontFormaGradova);
        
        this.crtajFormu(kontFormaGradova,this.kont);
        
    }


    crtajFormu(host,hostkont)
    {
        let SelectGrad = document.createElement("select");
        SelectGrad.className = "selectGrad";
        host.appendChild(SelectGrad);

        let OpcijeGrad;
        this.listagradova.forEach(p=>{
            OpcijeGrad = document.createElement("option");
            OpcijeGrad.innerHTML = p.naziv;
            OpcijeGrad.value = p.ID;
            SelectGrad.appendChild(OpcijeGrad);
        });

        var NoviGrad = document.createElement("div");
        NoviGrad.className = "novigrad";
        hostkont.appendChild(NoviGrad);
        var FormaF = document.createElement("div");
        FormaF.className = "formaF";
        hostkont.appendChild(FormaF);

        
        

        let dugmeNadjiFakultet = document.createElement("button")
        dugmeNadjiFakultet.onclick=(ev)=>
        {
            let element1 = document.body.getElementsByClassName("skripteknjigeizbor");
            if(element1[0]!=null)
            {
                document.body.removeChild(document.body.lastElementChild);
            }
            let element = document.body.getElementsByClassName("Izbor");
            if(element[0] != null)
            {
                document.body.removeChild(document.body.lastElementChild);
            }
            
            if(FormaF.hasChildNodes())
            {
                while(FormaF.firstChild)
                {
                    FormaF.removeChild(FormaF.firstChild);
                }
                
                this.NadjiFakultete(FormaF);
            }
            else{
                this.NadjiFakultete(FormaF);
            } 
        }

        dugmeNadjiFakultet.innerHTML = "Pretrazi";
        host.appendChild(dugmeNadjiFakultet);

        let dugmeDodajGrad = document.createElement("button")
        dugmeDodajGrad.onclick=(ev)=>{
            if(NoviGrad.hasChildNodes())
            {
                while(NoviGrad.firstChild)
                {
                    NoviGrad.removeChild(NoviGrad.firstChild);
                }
                
                this.CrtajDodajGrad(NoviGrad);
            }
            else{
                this.CrtajDodajGrad(NoviGrad);
            }
        }
        dugmeDodajGrad.innerHTML = "Dodaj grad";
        host.appendChild(dugmeDodajGrad);
        

    }
    

    CrtajDodajGrad(host)
    {


        let ulaz = document.createElement("input");
        ulaz.type = "text";
        ulaz.placeholder = " Grad";
        ulaz.className = "inputGrad";
        host.appendChild(ulaz);
        

        let dugmeDodaja = document.createElement("button")
        dugmeDodaja.onclick=(ev)=>{
            if(ulaz.value.length == 0)
            {
                alert("Morate uneti naziv grada");
            }
            else{
                this.DodajGrad(ulaz);
                
            }
        }
        dugmeDodaja.innerHTML = "Dodaj";
        host.appendChild(dugmeDodaja);

        let dugmeugasi = document.createElement("button")
        dugmeugasi.onclick=(ev)=>{
            while(host.firstChild)
            {
                host.removeChild(host.firstChild);
            }
        }
        dugmeugasi.innerHTML = "Prekini";
        host.appendChild(dugmeugasi);

    }
    DodajGrad(ulaz)
    {
        let naz = ulaz.value;
        let por =  0;
        for(let i=0;i<this.listagradova.length;i++)
        {
            if(this.listagradova[i].naziv == naz)
            {
                por++;
            }
        }
        if(por>0)
        {

            alert("Vec postoji");
        }
        else{
            fetch("https://localhost:5001/Grad/DodajGrad/"+naz,{method: 'POST'});
            
            document.location.reload(true);
                
            
        }

    }

    NadjiFakultete(host)
    {
        let opcija = this.kont.querySelector("select");
        var GradID = opcija.options[opcija.selectedIndex].value;

        let FaksDIV = document.createElement("div");
        FaksDIV.className = "faksdiv";
        host.appendChild(FaksDIV)

        let BoxD = document.createElement("button");
        BoxD.onclick=(ev)=>
        {
            let element = document.body.getElementsByClassName("skripteknjigeizbor");
            if(element[0]!=null)
            {
                document.body.removeChild(document.body.lastElementChild);
            }
            let element1 = document.body.getElementsByClassName("Izbor");
            if(element1[0]!=null)
            {
                document.body.removeChild(document.body.lastElementChild);
            }

            this.DodavanjeFakulteta(host);


            
        }
        BoxD.innerHTML = "Dodaj Fakultet";
        BoxD.className = "BoxFakultet";
        FaksDIV.appendChild(BoxD);
        
        this.IzaberiFakultet(GradID,FaksDIV);


    }

    IzaberiFakultet(ulaz,host)
    {
        let imp = ulaz;
        let listafakulteta = [];

        fetch("https://localhost:5001/Fakultet/PreuzmiFakultete/"+ulaz)
        .then(p=>{
            p.json().then(fakulteti =>{
                fakulteti.forEach(fakultet =>{
                    var z = new Fakultet(fakultet.id , fakultet.naziv);
                    listafakulteta.push(z);
                })
                
                if(listafakulteta[0] != null)
                {

                    let i=0;
                    while(i<listafakulteta.length)
                    {
                        console.log(listafakulteta[i]);

                        listafakulteta[i].crtajFakultet(host);
                        i++;
                    }
                    
                }
                else{
                    alert("Nema fakulteta za izabrani grad");
                }
            })

        })
    }

    DodavanjeFakulteta(host)
    {
        let opcija = this.kont.querySelector("select");
        var GradID = opcija.options[opcija.selectedIndex].value;
        
        let element = document.getElementsByClassName("Dodav");
        if(element[0])
        {
            host.removeChild(host.lastElementChild());

        }
        let kontformaDFakulteta = document.createElement("div");
        kontformaDFakulteta.className = "Dodav";
        host.appendChild(kontformaDFakulteta);



        let nazivFI = document.createElement("input");
        nazivFI.type = "text";
        nazivFI.placeholder = " Fakultet";
        nazivFI.className = "nazivFI";
        kontformaDFakulteta.appendChild(nazivFI);

        let dugmeDodajFaks = document.createElement("button");
        dugmeDodajFaks.onclick=(ev)=>
        {
            this.DODFaks(kontformaDFakulteta,nazivFI.value,GradID);

        }
        dugmeDodajFaks.innerHTML = "Dodaj Fakultet";
        dugmeDodajFaks.className = "DodajFaks";
        kontformaDFakulteta.appendChild(dugmeDodajFaks);

        let dugmeugasi = document.createElement("button")
        dugmeugasi.onclick=(ev)=>{
            host.removeChild(host.lastElementChild);

        }
        dugmeugasi.innerHTML = "Prekini";
        kontformaDFakulteta.appendChild(dugmeugasi);




    }

    DODFaks(host,naziv,gradId)
    {
        fetch("https://localhost:5001/Fakultet/DodajFakultet/"+naziv+"/"+gradId,{method: 'POST'})
        .then((response)=> {
            if(!response.ok){
                alert("Vec postoji");
            }
            else{
                document.location.reload(true);
            }
        })
        
    }
}