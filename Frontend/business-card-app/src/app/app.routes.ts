import { Routes } from '@angular/router';

export const routes: Routes = [

  {
    path: 'bsa',
    loadChildren: () =>
      import('./shared/shared.module').then((m) => m.SharedModule),
  },
  {
    path: '**',
    redirectTo: '/bsa',
    pathMatch: 'full',
  },
];
