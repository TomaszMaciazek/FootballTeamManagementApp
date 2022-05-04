import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { CreateTestComponent } from './components/create-test/create-test.component';
import { TestFillComponent } from './components/test-fill/test-fill.component';
import { TestResultsComponent } from './components/test-results/test-results.component';
import { TestUserResultComponent } from './components/test-user-result/test-user-result.component';
import { TestsAllComponent } from './components/tests-all/tests-all.component';
import { TestsAssignedToMeComponent } from './components/tests-assigned-to-me/tests-assigned-to-me.component';
import { TestsCreatedByMeComponent } from './components/tests-created-by-me/tests-created-by-me.component';

const routes: Routes = [
  { path: 'create', component: CreateTestComponent, canActivate: [AuthGuard]},
  { path: 'all', component: TestsAllComponent, canActivate: [AuthGuard]},
  { path: 'created', component: TestsCreatedByMeComponent, canActivate: [AuthGuard]},
  { path: 'assigned', component: TestsAssignedToMeComponent, canActivate: [AuthGuard]},
  { path: 'fill/:id', component: TestFillComponent, canActivate: [AuthGuard]},
  { path: 'result/:id', component: TestUserResultComponent, canActivate: [AuthGuard]},
  { path: 'results/all/:id', component: TestResultsComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
