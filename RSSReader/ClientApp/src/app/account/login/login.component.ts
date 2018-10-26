import { Component, OnInit } from '@angular/core';
import { LoginUser } from '../../auth/models/login-user.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginModel: LoginUser = { email : "", password : "" };
  public returnUrl: string;
  public loginForm: FormGroup;
  public loading: boolean = false;
  public errorMessage: string;

  constructor(private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute, private authService: AuthService) { }

  public ngOnInit() {
    this.initForm();
    this.authService.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public onSubmit(): void {

    if (this.loginForm.invalid) {
      return;
    }

    this.errorMessage = "";
    this.loading = true;

    this.authService.login(this.loginForm.controls.email.value, this.loginForm.controls.password.value)
      .pipe(first())
      .subscribe(data => {
        this.router.navigate([this.returnUrl]);
        this.loading = false;
    }, error => {
      this.loading = false;
      this.errorMessage = <string>error.error || "Sorry! Some error occured";
    })
  }

  public get email() {
    return this.loginForm.get("email");
  }

  public get password() {
    return this.loginForm.get("password");
  }

  private initForm(): void {
    this.loginForm = this.formBuilder.group({
      email: [this.loginModel.email, Validators.required],
      password: [this.loginModel.password, Validators.required]
    });
  }

}
