import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'ia-custom-checkbox',
    templateUrl: './custom-checkbox.component.html',
    styleUrls: ['./custom-checkbox.component.scss'],
})
export class CustomCheckboxComponent {

    // Cannot be used with ngModel, in a FormControl or FormGroup
    // For these scenarios, see: https://www.tsmean.com/articles/angular/custom-checkbox-component-with-angular/

    @Input() isChecked = false;
    @Input() isIndeterminate = false;
    @Output() onClick = new EventEmitter();

    constructor() {}

    checkboxClicked(event): void {
        this.onClick.emit(event);
    }
}
