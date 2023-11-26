import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  email: String = "";
  password: String = "";
  
  constructor(private client: HttpClient, private router: Router) {}


  login() {
    this.client.post("https://localhost:7023/login", {
      email: this.email,
      password: this.password
    })
    .subscribe({
      next: (data: any) => {
        console.log(data.token)

        localStorage.setItem("token", data.token);

        // this.router.navigate(["pages/dashboard/home"]);
      },

      error: (error) => {
        alert("Usuário ou senha incorretos")
      }
    })
  }
}
