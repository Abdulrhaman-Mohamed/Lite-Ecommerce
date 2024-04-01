import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";
import { LoginComponent } from "./login.component";
import { FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { CustomInputComponent } from '../../components/custom-input/custom-input.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
describe('LoginComponent_INIT', () => {
    let component: LoginComponent;
    let _authService: AuthService;
    let _Router: Router;
    let fixture: ComponentFixture<LoginComponent>;

    // beforeEach(async () => {
    //     await TestBed.configureTestingModule({
    //     })
    //     .compileComponents();
    //   });

    //   beforeEach(() => {
    //     fixture = TestBed.createComponent(LoginComponent);
    //     component = fixture.componentInstance;
    //     fixture.detectChanges();
    //   });

    beforeEach(() => {
        component = new LoginComponent(_authService ,_Router);
    });

    test('should create the app', () => {
        expect(component).toBeTruthy();
    });

    test("vaildators Init",()=>{
        expect(component.emailValidators).toBeTruthy();
        expect(component.passwordValidators).toBeTruthy();
        expect(component.emailValidators).toContain(Validators.required);
        expect(component.emailValidators).toContain(Validators.email);
    })

    test("Porperty",()=>{
        expect(component.vaild).toBeTruthy();
        expect(component.vaild.email).toBeTruthy();
        expect(component.vaild.password).toBeTruthy();
        expect(component.vaild.email.value).toBe('');
        expect(component.vaild.email.status).toBe(false);
        expect(component.vaild.password.value).toBe('');
        expect(component.vaild.password.status).toBe(false);
    })

});


describe('LoginComponent_Functions & ui', () => {
    let component: LoginComponent;

    let fixture: ComponentFixture<LoginComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [CustomInputComponent,
                ReactiveFormsModule,
                FormsModule ,
                MatIconModule,
                MatDividerModule,
                MatButtonModule,
                BrowserModule,
                CommonModule,
                BrowserAnimationsModule
                
              ],
              providers: [
                HttpClient,
                HttpHandler, // Provide real HttpClient
            ]
        })
        .compileComponents();
      });

      beforeEach(() => {
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
      });

      test("UI Checker",()=>{
        expect(fixture.nativeElement.querySelector('p').innerHTML).toBe('Login Form');
        expect(fixture.nativeElement.querySelector('app-custom-input')).toBeTruthy();
        expect(fixture.nativeElement.querySelector('button')).toBeTruthy();
      })
})