import { Component, OnDestroy, ElementRef, Renderer2 } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthApiService } from '../services/auth-api.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnDestroy {
  requestModel: FormGroup;
  AuthRegister!: Subscription;
  showPass: boolean = false;

  constructor(
    private formBuild: FormBuilder,
    private el: ElementRef,
    private renderer: Renderer2,
    private authRegister: AuthApiService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.requestModel = this.formBuild.group({
      first_name: ['', Validators.required],
      last_name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  errorChecker = (formName: any) => {
    const inputMap: any = {
      first_name: 'first-name',
      last_name: 'last-name',
      email: 'email',
      username: 'username',
      password: 'password',
    };

    const inputName = inputMap[formName];
    const inputElement = this.el.nativeElement.querySelector(`#${inputName}`);
    const errorInput = this.requestModel.get(formName)?.hasError('required');
    const errorEmail = this.requestModel.get(formName)?.hasError('email');

    if (errorInput || errorEmail) {
      this.renderer.setStyle(inputElement, 'border', '1px solid red');
    } else {
      this.renderer.setStyle(inputElement, 'border', '1px solid gray');
    }
  };

  viewPass = () => {
    this.showPass = !this.showPass;
  };

  formSubmit = () => {
    if (this.requestModel.valid) {
      this.AuthRegister = this.authRegister
        .register(this.requestModel.value)
        .subscribe({
          next: (data: any) => {
            this.requestModel.reset();
            this.router.navigate(['../login'], { relativeTo: this.route });
          },
          error: (error) => {
            console.error(error);
          },
        });
    }
  };

  ngOnDestroy(): void {
    if (this.AuthRegister) this.AuthRegister.unsubscribe();
  }
}
