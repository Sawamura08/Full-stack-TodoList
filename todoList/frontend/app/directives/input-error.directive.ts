import { Directive, ElementRef, Renderer2, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Directive({
  selector: '[appInputError]',
})
export class InputErrorDirective implements OnInit {
  @Input() formGroup: FormGroup = new FormGroup({});
  @Input() controlName!: string;
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {
    const control = this.formGroup.get(this.controlName);

    control?.statusChanges.subscribe(() => {
      if (control.invalid && control.touched) {
        this.renderer.addClass(this.el.nativeElement, 'error');
      } else {
        this.renderer.removeClass(this.el.nativeElement, 'error');
      }
    });

    // this.el.nativeElement.addEventListener('blur', () => this.blur());
  }

  blur() {
    const inputElement = this.el.nativeElement;
    const control = this.formGroup.get(this.controlName);

    if (control?.hasError('required')) {
      this.renderer.addClass(inputElement, 'error');
    }
  }
}
