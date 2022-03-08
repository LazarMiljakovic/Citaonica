export class Knjiga
{
    
    constructor(id,naziv,file)
    {
        this.id = id;
        this.naziv = naziv;
        this.file = file;
        this.kont = null;
        
    }

    CrtajKnjigu(host,predmet)
    {
        this.kont = document.createElement("div");
        this.kont.classList = "Knjiga";
        host.appendChild(this.kont);

        let KnjigeBtn = document.createElement("button");
        KnjigeBtn.innerHTML = this.naziv;
        KnjigeBtn.onclick=(ev)=>
        {
            this.Otvori(predmet);
        }
        KnjigeBtn.className = "knjigecheck";
        this.kont.appendChild(KnjigeBtn);
    }
    Otvori(predmet)
    {
        fetch("https://localhost:5001/Knjiga/PreuzmiKnjigu/"+predmet+"/"+this.naziv)
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