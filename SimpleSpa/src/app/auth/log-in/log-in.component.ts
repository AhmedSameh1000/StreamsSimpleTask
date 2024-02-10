import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../Services/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css',
})
export class LogInComponent {
  constructor(private AuthService: AuthService, private Router: Router) {}
  ngOnDestroy(): void {
    if (this.Obs1 != null || this.Obs1 != undefined) {
      this.Obs1.unsubscribe();
    }
  }
  ngOnInit(): void {
    this.CreatelogInForm();
  }
  LoginForm: FormGroup;
  CreatelogInForm() {
    this.LoginForm = new FormGroup({
      Email: new FormControl(null, [Validators.required, Validators.email]),
      Password: new FormControl(null, [Validators.required]),
    });
  }
  EmailorPasswordIsIncorrect = '';
  Obs1: any;
  Login() {
    if (this.LoginForm.invalid) {
      return;
    }
    this.Obs1 = this.AuthService.LogIn(this.LoginForm.value).subscribe({
      next: (res: any) => {
        this.AuthService.SaveTokens(res);
        this.Router.navigate(['list']);
        console.log(res);
      },
      error: (err: any) => {
        this.EmailorPasswordIsIncorrect = err.error.message;
      },
    });
  }
}
