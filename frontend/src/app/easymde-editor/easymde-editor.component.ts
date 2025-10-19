import { Component, ElementRef, forwardRef, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import EasyMDE from 'easymde';
import CodeMirror from 'codemirror';
import './custom-markdown-mode.js';

@Component({
  selector: 'app-easymde-editor',
  imports: [],
  template: `<textarea #easymdeContainer></textarea>`,
  styleUrl: './easymde-editor.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => EasyMdeEditorComponent ),
      multi: true,

    }
  ]
})
export class EasyMdeEditorComponent implements ControlValueAccessor {
  private easyMde : EasyMDE | null = null;
  @ViewChild('easymdeContainer') easyMdeTextArea!: ElementRef<HTMLTextAreaElement>;

  private onChange: (value: string) => void = () => { };
  private onTouched: () => void = () => { };

  ngOnInit() {

  }

  ngAfterViewInit() {

    this.easyMde = new EasyMDE({
      autofocus: true,
      blockStyles: {
        bold: "**",
        italic: "*",
      },
      unorderedListStyle: "-",
      element: this.easyMdeTextArea.nativeElement,
      forceSync: true,
      lineWrapping: true,
      spellChecker: false,
      syncSideBySidePreviewScroll: false,
      tabSize: 4,
      toolbar: false,
      status: false,
    });



    // TODO: Experiment with changing mode and using the on change event to change header class
    // this.easyMde.codemirror.setOption("mode", "custom-markdown");




    // create a local variable to ensure its not null by the
    // time its called by the change event
    const copyOfEasyMde : EasyMDE | null = this.easyMde;
    if (copyOfEasyMde != null) {
      // connect the easymde onchange to the angular onChange method
      copyOfEasyMde.codemirror.on('change', () => {
        this.adjustHeaderClass(copyOfEasyMde);
        this.onChange(copyOfEasyMde.value())
      })
    }
  }

  // Custom header sizing logic. Original EasyMDE made headers much larger than in preview
  adjustHeaderClass(copyOfEasyMde: EasyMDE) {
    const cm = copyOfEasyMde.codemirror;
    let removeClass = (line: CodeMirror.LineHandle) => {
      for (let i = 1; i <= 6; i++) {
        cm.removeLineClass(line, "wrap", `markdown-header-${i}`);
      }
    }
    cm.eachLine(line => {
      const text = cm.getLine(cm.getLineNumber(line)!);
      const match = text.match(/^(#{1,6})(?!\S)/);
      if (match) {
        const level = match[1].length;
        removeClass(line);
        cm.addLineClass(line, "wrap", `markdown-header-${level}`);
      } else {
        removeClass(line);
      }
    });


  }

  writeValue(value: string): void {
    if (this.easyMde !== null && value !== null) {
      this.easyMde.value(value);
    }
  }
  registerOnChange(fn: (value: string) => void) {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {}


}
