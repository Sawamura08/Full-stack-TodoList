import { Component, ElementRef, Renderer2, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthApiService } from '../services/auth-api.service';
import { AdminSharedDataService } from 'src/app/services/admin-shared-data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnDestroy {
  requestModel!: FormGroup;
  invalid: boolean = false;
  authSubscription!: Subscription;
  showPass: boolean = false;
  loadingCard: boolean = false;

  closeIncorrectCredentials = () => {
    this.invalid = false;
  };

  constructor(
    private formBuild: FormBuilder,
    private el: ElementRef,
    private renderer: Renderer2,
    private authApi: AuthApiService,
    private router: Router,
    private ActRoute: ActivatedRoute,
    private sharedData: AdminSharedDataService
  ) {
    this.requestModel = this.formBuild.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  show = () => {
    this.showPass = !this.showPass;
  };

  checkError = (formName: any) => {
    const inputIdMap: any = {
      username: 'username',
      password: 'password',
    };

    const inputName = inputIdMap[formName];
    const inputElement = this.el.nativeElement.querySelector(`#${inputName}`);
    const errorInput = this.requestModel.get(formName)?.hasError('required');

    if (errorInput) {
      this.renderer.setStyle(inputElement, 'border', '1px solid red');
    } else {
      this.renderer.setStyle(inputElement, 'border', '1px solid gray');
    }
  };

  sendApiLogin() {
    return this.authApi.login(this.requestModel.value);
  }

  onSubmit() {
    if (this.requestModel.valid) {
      this.authSubscription = this.sendApiLogin().subscribe({
        next: (data) => {
          this.sharedData.setSession('id', data.id);
          this.loading();
        },
        error: (error) => {
          console.error(error);
          console.log('Error');
        },
        complete: () => {
          setTimeout(() => {
            this.router.navigate(['../../admin/upcoming'], {
              relativeTo: this.ActRoute,
            });
          }, 2000);
        },
      });
    } else {
      console.log('invalide');
      console.log(this.requestModel);
    }
  }

  ngOnDestroy() {
    if (this.authSubscription) this.authSubscription.unsubscribe();
  }

  // loading card

  dotCounter: number = 0;
  dot = '';

  loading = () => {
    this.loadingCard = true;
    setInterval(() => {
      if (this.dotCounter < 3) {
        this.dot += ' . ';
        this.dotCounter++;
      } else {
        this.dotCounter = 0;
        this.dot = '';
      }
    }, 500);
  };

  // loading card
}
