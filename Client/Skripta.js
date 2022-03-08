export class Skripta
{
    
    constructor(id,naziv)
    {
        this.id = id;
        this.naziv = naziv;
        this.kont = null;
        
    }

    CrtajSkriptu(host,predmet)
    {
        this.kont = document.createElement("div");
        this.kont.classList = "Skripta";
        host.appendChild(this.kont);

        let SkriptaBtn = document.createElement("button");
        SkriptaBtn.innerHTML = this.naziv;
        SkriptaBtn.onclick=(ev)=>
        {
            this.Otvori(predmet);
        }
        SkriptaBtn.className = "skrpitacheck";
        this.kont.appendChild(SkriptaBtn);
    }

    Otvori(predmet)
    {
        fetch("https://localhost:5001/Skripta/PreuzmiKnjigu/"+predmet+"/"+this.naziv)
        .then(result =>{
            if(result.status != 200){
                throw new Error("Ne postoji");
            }
            return result.blob();
        })
        .then(data =>{
            console.log(data);

            var url = window.URL.createObjectURL(data);
            let anchor = document.createElement("a");
            anchor.className = "anc";
            anchor.href = url;
            anchor.download = this.naziv;
            anchor.click();

            window.URL.revokeObjectURL(url);
            let element = document.body.getElementsByClassName("anc");
            if(element[0]!=null)
            {
                document.removeChild(anchor);
            }
        })
        .catch(error => {
            console.log(error);
        })

    }
}