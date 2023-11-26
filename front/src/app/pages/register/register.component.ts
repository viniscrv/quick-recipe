import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  username: String = '';
  email: String = '';
  biography: String = '';
  password: String = '';
  password2: String = '';

  constructor(private client: HttpClient, private router: Router) {}

  register() {
    if (
      this.username == '' ||
      this.email == '' ||
      this.biography == '' ||
      this.password == ''
    ) {
      alert('Preencha todos os campos');
      return;
    }
    if (this.password !== this.password2) {
      alert('As senhas não coincidem');
      return;
    }

    this.client
      .post('https://localhost:7023/register', {
        name: this.username,
        email: this.email,
        biography: this.biography,
        password: this.password,
      })
      .subscribe({
        next: (data) => {
          alert('Conta criada com sucesso');

          this.router.navigate(['/login']);
        },

        error: (error) => {
          alert('Campos inválidos');
        },
      });
  }
}
