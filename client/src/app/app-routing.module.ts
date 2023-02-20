import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EnvelopeGridComponent } from './features/envelope-grid/envelope-grid.component';
import { HomeComponent } from './features/home/home.component';


const routes: Routes = [
  {path:'home', component:HomeComponent, pathMatch: 'full'},
  {path:'envelopes', component:EnvelopeGridComponent, pathMatch: 'full'},
  { path: '',   redirectTo: '/home', pathMatch: 'full' }, // redirect to 
  // {path:'**', component:HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
