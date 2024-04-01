import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field'
import {MatButtonModule} from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-custom-input',
  standalone: true,
  imports: [ReactiveFormsModule , MatIconModule, MatInputModule, MatFormFieldModule, MatButtonModule, CommonModule],
  providers: [],
  templateUrl: './custom-input.component.html',
  styleUrl: './custom-input.component.css'
})
export class CustomInputComponent implements OnInit{
  ngOnInit(): void {
    // console.log(this.validators);
    this.control.setValidators(this.validators);
  }



  hide = true;
  @Input() label: string = '';
  @Input() type: string = 'text';
  @Input() Hint: boolean = false;
  @Input() validators: ValidatorFn[] = [];
  @Output() valueChange = new EventEmitter<string>();
  @Output() ValidationChecker = new EventEmitter();


  control: FormControl = new FormControl('', this.validators);
  
  get value(): string {   
    return this.control.value;
  }

  set value(val: string) {
    this.control.setValue(val);
    this.valueChange.emit(val);
    
  }
  send(){
    this.valueChange.emit(this.control.value);
    this.ValidationChecker.emit(this.control.valid);
  }
  test(){
    console.log(this.control);
  }



}
