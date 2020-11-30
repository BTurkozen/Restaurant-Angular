import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  form: any;

  constructor(private authService: AuthService, private fb: FormBuilder) {
    this.form = fb.group(
      {
        username: ['', Validators.required],
        firsname: ['', Validators.required],
        lastname: ['', Validators.required],
        email: ['', Validators.required],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required],
      },
      { validators: matchingFields('password', 'confirmPassword') }
    );
  }
  onSubmit() {
    console.log(this.form.errors);
    this.authService.register(this.form.value);
  }

  isValid(control:any){
return this.form.controls[control].invalid && this.form.controls[control].touched;
  }
}
function matchingFields(field1: any, field2: any) {
  return (form) => {
    if (form.controls[field1].value !== form.controls[field2].value) {
      return { mismacthedFields: true };
    }
  };
}
