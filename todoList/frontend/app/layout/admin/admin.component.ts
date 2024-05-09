import {
  Component,
  ElementRef,
  OnInit,
  Renderer2,
  OnDestroy,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

import { AdminCreateApiService } from 'src/app/services/admin-create-api.service';
import { AdminSharedDataService } from 'src/app/services/admin-shared-data.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit, OnDestroy {
  toggle: boolean = false;
  requestModel: FormGroup;
  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
    private formBuild: FormBuilder,
    private adminApi: AdminCreateApiService,
    private sharedData: AdminSharedDataService,
    private router: Router,
    private ActRoute: ActivatedRoute
  ) {
    this.requestModel = this.formBuild.group({
      typeNames: ['', Validators.required],
      color: ['#00897B', Validators.required],
    });
  }

  // --------------------- ON INIT ---------------------

  id: number | undefined;
  ngOnInit(): void {
    this.id = this.sharedData.getSession('id');
    console.log(this.id);

    this.taskOnClick(0);
  }

  // --------------------- END OF ON INIT ---------------------

  // --------------------- NAV TOGGLE ---------------------
  navToggle = () => {
    const nav = this.el.nativeElement.querySelector('.nav');
    const hamburger = this.el.nativeElement.querySelector('.hamburger');
    const mainContainer = this.el.nativeElement.querySelector('.container');
    if (!this.toggle) {
      this.toggle = true;
      this.renderer.setStyle(nav, 'transform', 'translateX(-100%)');
      this.renderer.setStyle(mainContainer, 'transform', 'translateX(-20%)');
      this.renderer.setStyle(mainContainer, 'width', '90%');

      this.renderer.setStyle(hamburger, 'left', '2%');
    } else {
      this.toggle = false;
      this.renderer.setStyle(hamburger, 'left', '21%');
      this.renderer.setStyle(nav, 'transform', 'translateX(0%)');
      this.renderer.setStyle(mainContainer, 'transform', 'translateX(0%)');
      this.renderer.setStyle(mainContainer, 'width', '75%');
    }
  };

  // --------------------- task onclick ---------------------
  active: number = 0;
  taskOnClick = (index: number) => {
    const tasks = this.el.nativeElement.querySelectorAll(
      '.tasks'
    ) as NodeListOf<Element>;
    const counts = this.el.nativeElement.querySelectorAll(
      '.count'
    ) as NodeListOf<Element>;

    if (this.active != null) {
      //reset the style
      this.removeStyles(this.active, tasks);
    }

    this.active = index;

    // apply the style
    this.applyStyles(this.active, tasks);
  };

  applyStyles = (index: number, tasks: NodeListOf<Element>) => {
    this.renderer.addClass(tasks[index], 'task-active');
  };

  removeStyles = (index: number, tasks: NodeListOf<Element>) => {
    this.renderer.removeClass(tasks[index], 'task-active');
  };

  // --------------------- MODAL open ---------------------

  isModalOpen: boolean = false;

  modalCloser = (event: MouseEvent) => {
    event.stopPropagation(); // -  is like telling the event to stay where it is and not bother others. It's useful when you need to control event behavior, ensuring that events don't trigger unintended actions on parent elements.
    const clickedElement = event.target as HTMLElement;
    const modal = this.el.nativeElement.querySelector(
      '.add-list-container'
    ) as HTMLElement;

    if (modal && !modal.contains(clickedElement)) {
      this.isModalOpen = false;
    }
  };

  openModal = () => {
    this.isModalOpen = true;
  };

  // --------------------- MODAL INPUTS ---------------------
  AdminApiSubs!: Subscription;

  submitModalInput = () => {
    if (this.requestModel.valid) {
      this.AdminApiSubs = this.adminApi
        .createTaskType(this.requestModel.value)
        .subscribe({
          next: (data) => {
            console.log(data);
          },
          error: (error) => {
            console.error(error);
          },
        });
    } else {
      console.log('invalid');
    }
  };

  // --------------------- SIGN OU ---------------------

  signOutNow = () => {
    this.sharedData.clearSessionData('id');
    this.router.navigate(['../auth/login'], { relativeTo: this.ActRoute });
  };

  /* NG ON DESTROY */

  ngOnDestroy(): void {
    if (this.AdminApiSubs) this.AdminApiSubs.unsubscribe();
  }
}
