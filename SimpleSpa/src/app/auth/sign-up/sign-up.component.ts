import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../Services/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  constructor(private AuthService: AuthService, private Router: Router) {}
  ngOnDestroy(): void {
    if (this.Obs1 != null || this.Obs1 != undefined) {
      this.Obs1.unsubscribe();
    }
  }
  Obs1: any;
  ngOnInit(): void {
    this.CreatelogInForm();
  }
  RegisterForm: FormGroup;
  CreatelogInForm() {
    this.RegisterForm = new FormGroup({
      FullName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required]),
    });
  }
  Email_is_already_registered = '';
  Register() {
    if (this.RegisterForm.invalid) {
      return;
    }
    this.Obs1 = this.AuthService.Signup(this.RegisterForm.value).subscribe({
      next: (res: any) => {
        this.Router.navigate(['login']);
      },
      error: (err) => {
        console.log(err);
        this.Email_is_already_registered = err.error.message;
      },
    });
  }
}
