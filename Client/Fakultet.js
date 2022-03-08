import { Predmet } from "./Predmet.js";
import { Profesor } from "./Profesor.js";


export class Fakultet
{
    constructor(id,naziv)
    {
        this.id = id;
        this.naziv = naziv;
        this.kont = null;
        
    }

    crtajFakultet(host)
    {
        
        let BoxF = document.createElement("button");
        BoxF.onclick=(ev)=>
        {
            let element = document.body.getElementsByClassName("skripteknjigeizbor");
            if(element[0]!=null)
            {
                document.body.removeChild(document.body.lastElementChild);
            }
            

            this.Otvaranje(document.body);
        }
        BoxF.innerHTML = this.naziv;
        BoxF.className = "BoxFakultet";
        host.appendChild(BoxF);
        
        
        
    }

    Otvaranje(host)
    {
        let element = host.getElementsByClassName("Izbor");
        if(element[0] != null)
        {
            host.removeChild(host.lastElementChild);
            let kontformaFakulteta = document.createElement("div");
            kontformaFakulteta.className = "Izbor";
            host.appendChild(kontformaFakulteta);
            this.CrtajDugmeZaPP(kontformaFakulteta);
        }
        else{
            let kontformaFakulteta = document.createElement("div");
            kontformaFakulteta.className = "Izbor";
            host.appendChild(kontformaFakulteta);
            this.CrtajDugmeZaPP(kontformaFakulteta);

        }



    }

    CrtajDugmeZaPP(host)
    {
        

        let dugmeProfesor = document.createElement("button");
        dugmeProfesor.onclick=(ev)=>
        {
            this.UvozProfesora(host);

        }
        dugmeProfesor.innerHTML = "Profesori";
        dugmeProfesor.className = "PPDugme";
        host.appendChild(dugmeProfesor);

        let dugmePredmet = document.createElement("button");
        dugmePredmet.onclick=(ev)=>
        {
            this.UvozPredmeta(host);

        }
        dugmePredmet.innerHTML = "Predmeti";
        dugmePredmet.className = "PPDugme";
        host.appendChild(dugmePredmet);


    }

    UvozPredmeta(host)
    {
        let imp = this.id;
        let listaPredmeta = [];

        fetch("https://localhost:5001/Predmet/PreuzmiPredmete/"+imp)
        .then(p=>{
            p.json().then(predmeti =>{
                predmeti.forEach(predmet =>{
                    var a = new Predmet(predmet.id , predmet.naziv, predmet.godina );
                    listaPredmeta.push(a);
                })
                let element = document.body.getElementsByClassName("skripteknjigeizbor");
                if(element[0]!=null)
                {
                    document.body.removeChild(document.body.lastElementChild);
                }
                let element3 = document.getElementsByClassName("dod");
                if(element3[0]!=null)
                {
                    host.removeChild(host.lastElementChild);
                }


                let element2 = document.getElementsByClassName("BoxPP");
                if(element2[0]!=null)
                {
                    host.removeChild(host.lastElementChild);
                }


                let profesorDiv = this.ObrisiResetuj(host);
                
                let dugmeDodajPredmet = document.createElement("button")
                dugmeDodajPredmet.onclick=(ev)=>
                {
                    let elementa = document.getElementsByClassName("skripteknjigeizbor");
                    if(elementa[0]!=null)
                    {
                        document.body.removeChild(document.body.lastElementChild);
                        
                    }
                    this.Dodaj(host,listaPredmeta);
                    
                }
                dugmeDodajPredmet.innerHTML = "DodajPredmet";
                host.appendChild(dugmeDodajPredmet);
                if(predmeti[0]!= null)
                {
                    this.IzborGodine(profesorDiv,listaPredmeta);
                    this.CrtajDugmePretrazi(profesorDiv,listaPredmeta);
                    
                }
                else{
                    alert("Nema predmeta za izabrani Fakultet");
                }
            })

        })


    }

    UvozProfesora(host)
    {
        let imp = this.id;
        let listaProfesora = [];

        fetch("https://localhost:5001/Profesor/PreuzmiProfesore/"+imp)
        .then(p=>{
            p.json().then(profesori =>{
                profesori.forEach(profesor =>{
                    var b = new Profesor(profesor.id , profesor.ime, profesor.prezime, profesor.email,profesor.kancelarija );
                    listaProfesora.push(b);
                })
                let element = document.body.getElementsByClassName("skripteknjigeizbor");

                if(element[0]!=null)
                {
                    document.body.removeChild(document.body.lastElementChild);
                }    
                let element3 = document.getElementsByClassName("dod");
                if(element3[0]!=null)
                {
                    host.removeChild(host.lastElementChild);
                }

                let element2 = document.getElementsByClassName("BoxPP");
                if(element2[0]!=null)
                {
                    host.removeChild(host.lastElementChild);
                }

                
                let profesorDiv = this.ObrisiResetuj(host);
                
                



                let dugmeDodajProf = document.createElement("button");
                dugmeDodajProf.onclick=(ev)=>
                {
                    this.DodajProof(profesorDiv,listaProfesora);

                }
                dugmeDodajProf.innerHTML = "Dodaj Profesora";
                dugmeDodajProf.className = "DodajProf";
                profesorDiv.appendChild(dugmeDodajProf);

                if(profesori[0]!= null)
                {
                    let i=0;
                    while(i<listaProfesora.length)
                    {
                        console.log(listaProfesora[i]);
                        

                        listaProfesora[i].crtajProfesora(profesorDiv);
                        i++;
                    }
                    
                }
                else{
                    alert("Nema profesora za izabrani Fakultet");
                }
            })

        })


    }

    ObrisiResetuj(host)
    {
        let element = host.getElementsByClassName("BoxPP");
        if(element[0]!=null)
        {
            host.removeChild(host.lastElementChild);
            
                    
        }

        let profesorDiv = document.createElement("div");
        profesorDiv.className = "BoxPP";
        host.appendChild(profesorDiv);

        return profesorDiv;
    }
    IzborGodine(host, lista)
    {
        let izbGodinu = document.createElement("label");
        izbGodinu.innerHTML = "Izaberi godinu: ";
        host.appendChild(izbGodinu);

        let SelectGodinu = document.createElement("select");
        host.appendChild(SelectGodinu);
        
        let j = 0;
        let listaGodina = [];
        for(let i =0;i<lista.length;i++)
        {
            if(!listaGodina.includes(lista[i].godina))
            {
                listaGodina[j] = lista[i].godina;
                j++;
                

            }
        }

        let OpcijeGodine;
        OpcijeGodine = document.createElement("option");
        OpcijeGodine.innerHTML = "Sve";
        OpcijeGodine.value = "0";
        SelectGodinu.appendChild(OpcijeGodine);
        for(let i =0;i<j;i++)
        {
            OpcijeGodine = document.createElement("option");
            OpcijeGodine.innerHTML = listaGodina[i];
            OpcijeGodine.value = listaGodina[i];
            SelectGodinu.appendChild(OpcijeGodine);
        }
    }

    CrtajDugmePretrazi(host,listaPredmeta)
    {
        let dugmeNadjiPredmet = document.createElement("button")
        dugmeNadjiPredmet.onclick=(ev)=>
        {
            let element1 = document.getElementsByClassName("dod");
            if(element1[0]!=null)
            {
                host.removeChild(host.lastElementChild);
                
            }
            let element = host.getElementsByClassName("predmetBA")
            if(element[0] != null)
            {
                host.removeChild(host.lastElementChild)
                    
            }
            let elementa = document.getElementsByClassName("skripteknjigeizbor");
            if(elementa[0]!=null)
            {
                document.body.removeChild(document.body.lastElementChild);
                
            }
            let predmetB = document.createElement("div");
            predmetB.classList = "predmetBA";
            host.appendChild(predmetB);
            this.NadjiPredmete(host,predmetB,listaPredmeta);
             
        }
        dugmeNadjiPredmet.innerHTML = "Pretrazi";
        host.appendChild(dugmeNadjiPredmet);

        
    }

    NadjiPredmete(zaOpcije,host,listaPredmeta)
    {
        let opcija = zaOpcije.querySelector("select");
        var Godina = opcija.options[opcija.selectedIndex].value;
        let i=0;

        console.log(Godina);
        
        while(i<listaPredmeta.length)
        {
            if(Godina == 0)
            {
                listaPredmeta[i].crtajPredmet(host);
                i++;
            }
            else
            {
                if(listaPredmeta[i].godina == Godina)
                {
                    listaPredmeta[i].crtajPredmet(host);
                    i++;
                }
                else{
                    i++;
                }

            }
            
        }

    }

    Dodaj(host,listaPredmeta)
    {
        let element = document.getElementsByClassName("dod");
        if(element[0]!=null)
        {
            host.removeChild(host.lastElementChild);
            if(element[1]!=null)
            {
                host.removeChild(host.lastElementChild);
            }
        }
        let dod = document.createElement("div");
        dod.className = "dod";
        host.appendChild(dod);

        let n = document.createElement("label");
        n.innerHTML = "Ime Predmeta";
        n.className = "labpredmet";
        dod.appendChild(n);

        let ulaz = document.createElement("input");
        ulaz.className = "inputpredmet";
        dod.appendChild(ulaz);

        let SelectG = document.createElement("select");
        dod.appendChild(SelectG);
        let i = 0
        for(i;i<6;i++)
        {
            let G;
            G = document.createElement("option");
            G.innerHTML = i;
            G.value = i;
            SelectG.appendChild(G);

        }

        let uploadNaServer = document.createElement("button");
        uploadNaServer.innerHTML = "Dodaj";
        uploadNaServer.onclick=(ev)=>
        {
            if(ulaz.value.length == 0)
            {
                alert("Morate uneti naziv predmeta");
            }
            else{
                this.unesi(host,ulaz.value,listaPredmeta);                
            }
        }
        uploadNaServer.className = "upludujpredmet";
        dod.appendChild(uploadNaServer);

        let dugmeugasi = document.createElement("button")
        dugmeugasi.onclick=(ev)=>{
            while(dod.firstChild)
            {
                dod.removeChild(dod.firstChild);
            }
        }
        dugmeugasi.innerHTML = "Prekini";
        dod.appendChild(dugmeugasi);

    }

    unesi(host,naz,listaPredmeta)
    {
        let opcija = host.querySelector("select");
        var G = opcija.options[opcija.selectedIndex].value;

        let por =  0;
        for(let i=0;i<listaPredmeta.length;i++)
        {
            if(listaPredmeta[i].naziv == naz)
            {
                por++;
            }
        }
        if(por>0)
        {

            alert("Vec postoji");
        }
        else{
            fetch("https://localhost:5001/Predmet/DodajPredmet/"+naz+"/"+G+"/"+this.id,{method: 'POST'});
            document.location.reload(true);
                
            
        }
        

    }

    DodajProof(host,listaProfesora)
    {
        let element = document.getElementsByClassName("DodajProf")
        if(element[0]!=null)
        {
            while(host.lastElementChild)
            {
                host.removeChild(host.lastElementChild);
            }
        }
        let DP = document.createElement("label");
        DP.innerHTML = "Dodaj Profesora:";
        DP.className = "DP";
        host.appendChild(DP);

        let ime = document.createElement("label");
        ime.innerHTML = "Ime";
        ime.className = "ime";
        host.appendChild(ime);

        let imeI = document.createElement("input");
        imeI.className = "imeI";
        host.appendChild(imeI);

        let prezime = document.createElement("label");
        prezime.innerHTML = "Prezime";
        prezime.className = "prezime";
        host.appendChild(prezime);

        let prezimeI = document.createElement("input");
        prezimeI.className = "prezimeI";
        host.appendChild(prezimeI);

        let email = document.createElement("label");
        email.innerHTML = "email";
        email.className = "email";
        host.appendChild(email);

        let emailI = document.createElement("input");
        emailI.className = "emailI";
        host.appendChild(emailI);

        let Kanc = document.createElement("label");
        Kanc.innerHTML = "Kancelarija";
        Kanc.className = "Kanc";
        host.appendChild(Kanc);

        let KancI = document.createElement("input");
        KancI.className = "KancI";
        host.appendChild(KancI);

        let listaPredmeta = [];

        fetch("https://localhost:5001/Predmet/PreuzmiPredmete/"+this.id)
        .then(p=>{
            p.json().then(predmeti =>{
                predmeti.forEach(predmet =>{
                    var w = new Predmet(predmet.id , predmet.naziv, predmet.godina );
                    listaPredmeta.push(w);
                })

                let izbPred = document.createElement("label");
                izbPred.innerHTML = "Izaberi predmet: ";
                host.appendChild(izbPred);

                let SelecPred = document.createElement("select");
                host.appendChild(SelecPred);
                
                let OpcijePred;
                console.log(listaPredmeta);

                listaPredmeta.forEach(p=>{
                    OpcijePred = document.createElement("option");
                    OpcijePred.innerHTML = p.naziv;
                    OpcijePred.value = p.id;
                    SelecPred.appendChild(OpcijePred);
                });


                let dugmeDodajProfS = document.createElement("button");
                dugmeDodajProfS.onclick=(ev)=>
                {
                    this.DodajProfHost(host,imeI,prezimeI,emailI,KancI,listaProfesora);

                }
                dugmeDodajProfS.innerHTML = "Dodaj";
                dugmeDodajProfS.className = "DodajProfs";
                host.appendChild(dugmeDodajProfS);

                let dugmeugasi = document.createElement("button")
                dugmeugasi.onclick=(ev)=>{
                    while(host.firstChild)
                    {
                        host.removeChild(host.firstChild);
                    }
                }
                dugmeugasi.innerHTML = "Prekini";
                host.appendChild(dugmeugasi);

            })
        })
        

        
    }

    DodajProfHost(host,imeI,prezimeI,emailI,KancI,listaProfesora)
    {

        let opcija = host.querySelector("select");
        var PredID = opcija.options[opcija.selectedIndex].value;

        let por =  0;
        for(let i=0;i<listaProfesora.length;i++)
        {
            if(listaProfesora[i].email == emailI.value)
            {
                por++;
            }
        }
        if(por>0)
        {

            alert("Vec postoji");
        }
        else{
            if(imeI.value==null & prezimeI.value==null)
            {
                alert("niste uneli ime i prezime");
            }
            if(emailI.value==null)
            {
                alert("niste uneli email");
            }
            if(KancI.value==null)
            {
                alert("niste uneli kancelariju");
            }
            else{
            fetch("https://localhost:5001/Profesor/DodajProfesora/"+imeI.value+"/"+prezimeI.value+"/"+this.id+"/"+PredID+"/"+emailI.value+"/"+KancI.value,{method: 'POST'});
            
            document.location.reload(true);
            }
            
        }


    }
}