import { ComponentFixture, TestBed } from "@angular/core/testing";
import { CustomInputComponent } from "./custom-input.component";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatIconModule } from "@angular/material/icon";
import { ReactiveFormsModule, Validators } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



describe('CustomInputComponent_INIT', () => {
    let component: CustomInputComponent;


    beforeEach(() => {
        component = new CustomInputComponent();
    });

    test('should create the app', () => {
        expect(component).toBeTruthy();
    });

    test("vaildators Init",()=>{
        expect(component.validators).toBeTruthy();
        
    })

    test("Porperty",()=>{
        expect(component.control).toBeTruthy();
        expect(component.control.value).toBe('');
        expect(component.control.valid).toBe(true);
    })
});

describe('CustomInputComponent_Functions & ui', () => {
    let component: CustomInputComponent;

    let fixture: ComponentFixture<CustomInputComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                ReactiveFormsModule
                 , MatIconModule
                 , MatInputModule
                 , MatFormFieldModule
                 , MatButtonModule
                 , CommonModule
                 ,BrowserAnimationsModule
              ],
              providers: [ ]
        })
        .compileComponents();
      });

      beforeEach(() => {
        fixture = TestBed.createComponent(CustomInputComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
      });

      test('should create the app', () => {
            expect(component).toBeTruthy();
            expect(fixture.nativeElement.querySelector("mat-form-field")).toBeTruthy();
            expect(fixture.nativeElement.querySelector("mat-label")).toBeTruthy();
            expect(fixture.nativeElement.querySelector("input")).toBeTruthy();
            component.Hint = true;
            fixture.detectChanges();
            expect(fixture.nativeElement.querySelector("mat-hint")).toBeTruthy();
            component.type = 'password';
            fixture.detectChanges();
            expect(fixture.nativeElement.querySelector("button")).toBeTruthy();
      });

      test("vaildators Control",()=>{
        component.validators = [Validators.required];
        // component.ngOnInit();
        // expect(component.control.validator?.length).toBe(1);
      })

      test("Check Email", ()=>{
        component.validators = [Validators.required , Validators.email];
        component.ngOnInit();
        component.control.setValue('test');
        expect(fixture.nativeElement.querySelector("input").value).toBe('test');
        component.control.setValue('');
        expect(component.control.valid).toBe(false);
        component.control.setValue('abdo@gmail.com');
        expect(component.control.valid).toBe(true);
      })

      test("Check Password", ()=>{
        component.validators = [Validators.required ,Validators.pattern('(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}')];
        component.ngOnInit();
        component.control.setValue('');
        expect(component.control.valid).toBe(false);
        component.control.setValue('123456');
        expect(component.control.valid).toBe(false);
        component.control.setValue('Abdo@12345');
        expect(component.control.valid).toBe(true);
      })


      test("error message Email",()=>{
        component.validators = [Validators.required , Validators.email];
        component.ngOnInit();
        component.control.setValue('');
        expect(component.control.valid).toBe(false);
        component.control.setValue('');
        component.control.markAsTouched();
        fixture.whenStable().then(()=>{
            expect(fixture.nativeElement.querySelector("mat-error").innerHTML).toBe("This field is required.");
        })
        component.control.setValue('heloo');
        component.control.markAsTouched();
        fixture.whenStable().then(()=>{
            expect(fixture.nativeElement.querySelector("mat-error").innerHTML).toBe("Invilad Email");
        })
      })

      test("error message Password",()=>{
        component.validators = [Validators.required ,Validators.pattern('(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}')];
        component.ngOnInit();
        component.control.setValue('');
        expect(component.control.valid).toBe(false);
        component.control.setValue('');
        component.control.markAsTouched();
        fixture.whenStable().then(()=>{
            expect(fixture.nativeElement.querySelector("mat-error").innerHTML).toBe("This field is required.");
        })
        component.control.setValue('123456');
        component.control.markAsTouched();
        fixture.whenStable().then(()=>{
            expect(fixture.nativeElement.querySelector("mat-error").innerHTML).toBe("Password must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters");
        })
      })

})