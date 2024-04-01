import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { CustomInputComponent } from '../../components/custom-input/custom-input.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CustomInputComponent, MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  /**
   *
   */
  constructor(private _Router: Router, private _authService: AuthService) {

  }
  emailValidators = [Validators.required, Validators.email];
  passwordValidators = [Validators.required, Validators.pattern('(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}')];
  userNameValidators = [Validators.required, Validators.minLength(3), Validators.maxLength(16)];
  vaild = {
    email: {
      value: '',
      status: false
    },
    password: {
      value: '',
      status: false
    },
    userName: {
      value: '',
      status: false
    }
  };


  checkEmail(e: any) {
    this.vaild.email.status = e;
  }
  checkPassword(e: any) {
    this.vaild.password.status = e;
  }

  getEmail(event: any) {
    this.vaild.email.value = event;
  }

  getPassword(event: any) {
    this.vaild.password.value = event;
  }
  checkUserName(e: any) {
    this.vaild.userName.status = e;
  }
  getUserName(event: any) {
    this.vaild.userName.value = event;
  }

  register(e: any) {
    e.preventDefault();
    if (this.vaild.email.status &&
      this.vaild.password.status &&
      this.vaild.userName.status) {
      this._authService.register({
        username: this.vaild.userName.value,
        password: this.vaild.password.value,
        email: this.vaild.email.value
      }).subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          this._Router.navigate(['/login']);
        }
      });
    }
  }
}
