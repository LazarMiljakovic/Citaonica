export class Profesor
{
    constructor(id,ime,prezime,email,kancelarija)
    {
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.kancelarija = kancelarija;
        this.kont = null;
        
    }
    

    crtajProfesora(host)
    {
        let ProfesorLab = document.createElement("label");
        ProfesorLab.className = "profesor";
        ProfesorLab.innerHTML = this.ime + "    " + this.prezime + "        email:"+ this.email + "     Kancelarija:"+this.kancelarija;
        host.appendChild(ProfesorLab);

    }

}