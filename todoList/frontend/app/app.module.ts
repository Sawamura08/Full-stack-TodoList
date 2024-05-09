import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxColorsModule } from 'ngx-colors';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminComponent } from './layout/admin/admin.component';
import { AuthComponent } from './layout/auth/auth.component';

import { LoginModule } from './views/auth/login/login.module';
import { RegisterModule } from './views/auth/register/register.module';
import { InputErrorDirective } from './directives/input-error.directive';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    AuthComponent,
    InputErrorDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LoginModule,
    RegisterModule,
    BrowserAnimationsModule,
    NgxColorsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [InputErrorDirective],
})
export class AppModule {}
