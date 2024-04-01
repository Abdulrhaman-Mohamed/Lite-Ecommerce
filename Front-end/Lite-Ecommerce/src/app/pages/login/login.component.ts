import { Component, OnInit } from '@angular/core';
import { CustomInputComponent } from '../../components/custom-input/custom-input.component';
import { CustomButtonComponent } from '../../components/custom-button/custom-button.component';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CustomInputComponent,
    CustomButtonComponent,
    ReactiveFormsModule,
    FormsModule ,
    MatIconModule,
    MatDividerModule,
    MatButtonModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  emailValidators = [Validators.required, Validators.email];
  passwordValidators = [Validators.required, Validators.pattern('(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}')];
  vaild={
    email:{
      value: '',
      status: false
    },
    password: {
      value: '',
      status: false
    },
  };

  constructor(private fb: FormBuilder , private _authService: AuthService , private _Router:Router) {

  }

  ngOnInit() {
  }

  checkEmail(e:any){
    this.vaild.email.status = e;
  }
  checkPassword(e:any){
    this.vaild.password.status = e;
  }

  getEmail(event:any){
    this.vaild.email.value = event;
  }

  getPassword(event:any){
    this.vaild.password.value = event;
    }
  login(){
    if(this.vaild.email.status && this.vaild.password.status){      
      this._authService.login({email:this.vaild.email.value,password:this.vaild.password.value}).subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          this._Router.navigate(['/home']);
        }
      })
    }
  }

  getP(){
    console.log('getP');
    
    this._authService.product().subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {

      }
    })
  }
}
