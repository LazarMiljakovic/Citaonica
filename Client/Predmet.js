import { Knjiga } from "./Knjiga.js";
import { Skripta } from "./Skripta.js";



export class Predmet
{
    
    constructor(id,naziv,godina)
    {
        this.id = id;
        this.naziv = naziv;
        this.godina = godina;
        this.kont = null;
        
    }

    crtajPredmet(host)
    {
        this.kont = document.createElement("div");
        this.kont.classList = "predmetB";
        host.appendChild(this.kont);


        let PredmetBtn = document.createElement("button");
        PredmetBtn.innerHTML = this.naziv;
        PredmetBtn.onclick=(ev)=>
        {
            this.CrtajFormu();

        }
        PredmetBtn.className = "predmetCheck";
        this.kont.appendChild(PredmetBtn);
        

    }

    CrtajFormu()
    {
        let element = document.body.getElementsByClassName("skripteknjigeizbor");

        if(element[0]!=null)
        {
            document.body.removeChild(document.body.lastElementChild);
        }
        this.kont = document.createElement("div");
        this.kont.className = "skripteknjigeizbor";
        document.body.appendChild(this.kont);

        let izborKnjSkr = document.createElement("div");
        izborKnjSkr.className = "izborKnjSkr";
        this.kont.appendChild(izborKnjSkr);

        let l1 = document.createElement("label");
        l1.innerHTML = "Knjige";
        izborKnjSkr.appendChild(l1);

        let knjigecb = document.createElement("input");
        knjigecb.type = "checkbox";
        knjigecb.value = "knjige";
        izborKnjSkr.appendChild(knjigecb);

        let l2 = document.createElement("label");
        l2.innerHTML = "Skripte";
        izborKnjSkr.appendChild(l2);

        let skriptecb = document.createElement("input");
        skriptecb.type = "checkbox";
        skriptecb.value = "skripte";
        izborKnjSkr.appendChild(skriptecb);

        let KnjSkrBtn = document.createElement("button");
        KnjSkrBtn.innerHTML = "Pretrazi";
        KnjSkrBtn.onclick=(ev)=>
        {
            this.Opcije(izborKnjSkr,KSforma);

        }
        KnjSkrBtn.className = "PretraziKnjigeSkripte";
        izborKnjSkr.appendChild(KnjSkrBtn);

        

        let uploadBtn = document.createElement("button");
        uploadBtn.innerHTML = "Dodaj";
        uploadBtn.onclick=(ev)=>
        {
            let element = this.kont.getElementsByClassName("up");
            if(element[0]!=null)
            {
                this.kont.removeChild(this.kont.lastElementChild);
            }   
            let up = document.createElement("div");
            up.className = "up";
            this.kont.appendChild(up);
            this.salji(up);

        }
        uploadBtn.className = "upluduj";
        izborKnjSkr.appendChild(uploadBtn);
        

        let KSforma = document.createElement("div");
        KSforma.className = "ksforma";
        this.kont.appendChild(KSforma); 
    }

    Opcije(host,KSforma)
    {
        var knjskr = host.querySelectorAll("input[type='checkbox']:checked");

               

        let i = 0;
        let s = 0;
        while(i<knjskr.length)
        {
            s = s+ knjskr[i].value;
            i++;

        }
        if(s == "0knjigeskripte")
        {
            this.ObrisiPripremi(KSforma)
            this.UnosKnjiga(KSforma);
            this.UnosSkripti(KSforma);
        }
        else if(s == "0knjige")
        {
            this.ObrisiPripremi(KSforma)
            this.UnosKnjiga(KSforma);
        }
        else if(s == "0skripte")
        {
            this.ObrisiPripremi(KSforma)
            this.UnosSkripti(KSforma);
        }
        else
        {
            alert("Izberite sta zelite pretraziti");
        }
        
        

    }

    UnosKnjiga(host)
    {
        let imp = this.id;
        let listaKnjiga = [];

        fetch("https://localhost:5001/Knjiga/PreuzmiKnjige/"+imp)
        .then(p=>{
            p.json().then(knjige =>{
                knjige.forEach(knjiga =>{
                    var k = new Knjiga(knjiga.id , knjiga.naziv,knjiga.file);
                    listaKnjiga.push(k);
                })
                

                if(listaKnjiga[0]!= null)
                {
                    let i = 0;
                    while(i<listaKnjiga.length)
                    {
                        listaKnjiga[i].CrtajKnjigu(host,this.id);
                        i++;
                    }
                    
                }
                else{
                    alert("Nema knjiga za izabrani Fakultet");
                }


            })

        })  

    }
    
    UnosSkripti(host)
    {
        let imp = this.id;
        let listaSkripti = [];

        fetch("https://localhost:5001/Skripta/PreuzmiSkripte/"+imp)
        .then(p=>{
            p.json().then(skripte =>{
                skripte.forEach(skripta =>{
                    var k = new Skripta(skripta.id , skripta.naziv);
                    listaSkripti.push(k);
                })
                
                if(listaSkripti[0]!= null)
                {
                    let i = 0;
                    while(i<listaSkripti.length)
                    {
                        listaSkripti[i].CrtajSkriptu(host,this.id);
                        i++;
                        
                    }
                    
                }
                else{
                    alert("Nema knjiga za izabrani Fakultet");
                }


            })

        })  


    }

    ObrisiPripremi(host)
    {
        while(host.lastElementChild)
        {
            host.removeChild(host.lastElementChild);
        }

    }

    salji(host)
    {
        let SelectSK = document.createElement("select");
        host.appendChild(SelectSK);

        let OpK;
        OpK = document.createElement("option");
        OpK.innerHTML = "Knjige";
        OpK.value = "0";
        SelectSK.appendChild(OpK);

        let OpS;
        OpS = document.createElement("option");
        OpS.innerHTML = "Skripte";
        OpS.value = "1";
        SelectSK.appendChild(OpS);


        let upload = document.createElement("input");
        upload.type = "file";
        upload.className = "upload";
        host.appendChild(upload);

        let uploadNaServer = document.createElement("button");
        uploadNaServer.innerHTML = "Dodaj";
        uploadNaServer.onclick=(ev)=>
        {
            this.unesi(host,upload);
        }
        uploadNaServer.className = "upluduj";
        host.appendChild(uploadNaServer);

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

    unesi(host,upload)
    {
        let opcija = host.querySelector("select");
        var KS = opcija.options[opcija.selectedIndex].value;

        if(upload.files[0]!= null)
        {
            if(KS==0)
            {

                this.slanjeK(upload.files[0]);

            }
            else{
                this.slanjeS(upload.files[0]);
            }

        }
        else
        {
            alert("Niste uneli fajl");
        }
    }

    slanjeK(file)
    {
        var formData = new FormData();
        formData.append("file",file);
        
        fetch("https://localhost:5001/Knjiga/DodajKnjigu/"+this.id,{
            method: "post",
            body: formData,
        }).catch((error) =>("Greska",error));
        

    }

    slanjeS(file)
    {
        var formData = new FormData();
        formData.append("file",file);
        
        fetch("https://localhost:5001/Skripta/DodajSkriptu/"+this.id,{
            method: "post",
            body: formData,
        }).catch((error) =>("Greska",error));
        

    }

}
