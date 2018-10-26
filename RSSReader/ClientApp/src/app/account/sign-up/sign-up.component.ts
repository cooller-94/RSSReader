import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { first } from 'rxjs/operators';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  public signUpForm: FormGroup;
  public loading: boolean = false;
  public errorMessage: string;

  constructor(private formBuilder: FormBuilder, private router: Router, private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.initForm();
  }

  public onSubmit(): void {
    if (this.signUpForm.invalid) {
      return;
    }

    this.errorMessage = "";
    this.loading = true;

    this.userService.register(this.email.value, this.password.value, this.fullName.value)
      .pipe(first())
      .subscribe(data => {
        this.login();
      }, error => {
        this.errorMessage = error.error;
      });
  }

  public get email() {
    return this.signUpForm.get("email");
  }

  public get password() {
    return this.signUpForm.get("password");
  }

  public get fullName() {
    return this.signUpForm.get("fullName");
  }

  private initForm(): void {
    this.signUpForm = this.formBuilder.group({
      email: ["", Validators.required],
      password: ["", Validators.required],
      fullName: ["", Validators.required],
    });
  }

  private login(): void {
    this.authService.login(this.email.value, this.password.value)
      .pipe(first())
      .subscribe(ld => {
        this.router.navigate(["/latest"])
      }, error => { this.errorMessage = error.error });
  }

}
