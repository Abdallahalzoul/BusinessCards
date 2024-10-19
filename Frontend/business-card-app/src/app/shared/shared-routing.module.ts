import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessCardListComponent } from '../components/business-card-list/business-card-list.component';
import { BusinessCardAddComponent } from '../components/business-card-add/business-card-add.component';
import { SharedComponent } from './shared.component';
import { BusinessCardImportComponent } from '../components/business-card-import/business-card-import.component';

const routes: Routes = [

  {
    path:"",
    component:SharedComponent,
    children:[
      { path: 'list', component: BusinessCardListComponent },
      { path: 'add', component: BusinessCardAddComponent },
      { path: 'import', component: BusinessCardImportComponent },
      { path: '**', redirectTo: '/bsa/list', pathMatch: 'full' },

    ]
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
