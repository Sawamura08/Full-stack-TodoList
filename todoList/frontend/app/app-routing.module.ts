import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './layout/auth/auth.component';
import { LoginComponent } from './views/auth/login/login.component';
import { AdminComponent } from './layout/admin/admin.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/auth/login',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    component: AuthComponent,
    children: [
      {
        path: 'login',
        loadChildren: () =>
          // loadChildren it allows load a code when it only neede = lazy loading reduce loading time
          import('./views/auth/login/login.module').then((m) => m.LoginModule),
      },
      {
        path: 'register',
        loadChildren: () =>
          import('./views/auth/register/register.module').then(
            (m) => m.RegisterModule
          ),
      },
    ],
  },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      {
        path: 'upcoming',
        loadChildren: () =>
          import('./views/admin/upcoming/upcoming.module').then(
            (m) => m.UpcomingModule
          ),
      },
      {
        path: 'today',
        loadChildren: () =>
          import('./views/admin/today/today.module').then((m) => m.TodayModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
