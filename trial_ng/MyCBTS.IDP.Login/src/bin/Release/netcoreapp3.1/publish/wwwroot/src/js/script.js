//Form validation
(function() {
    'use strict';
    window.addEventListener('load', function() {
      // Fetch all the forms we want to apply custom Bootstrap validation styles to
      var forms = document.getElementsByClassName('needs-validation');
      // Loop over them and prevent submission
      var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
          if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
          }
          form.classList.add('was-validated');
        }, false);
      });
    }, false);
  })();

  
//ie11 and ie10 form validation
  var forms = document.getElementsByClassName('needs-validation');
  if(forms){
    if(navigator.appVersion.indexOf('MSIE 10') !== -1 || !!window.MSInputMethodContext && !!document.documentMode){
      forms.forEach( form => {
        //form.removeAttribute('novalidate');
        console.log(form);
      });
    }
  }


  //toggle switch functionality
  if(navigator.appVersion.indexOf('MSIE 10') !== -1 || !!window.MSInputMethodContext && !!document.documentMode){
    (function(){
      return; //if ie10 or ie11
    })()
  } else {
    const toggleComponents = document.querySelectorAll('.toggle-wrapper');
    toggleComponents.forEach(toggleComponent => {
      const on = toggleComponent.querySelector('.toggle-on');
      const off = toggleComponent.querySelector('.toggle-off');
      const btn = toggleComponent.querySelector('.cb-checkbox-for-toggle');
      const inputChecked = btn.hasAttribute('checked');
      inputChecked || btn.querySelector('.checked') ? off.classList.toggle('d-none') : on.classList.toggle('d-none');
      if(btn){
        btn.addEventListener('click', () => {
          btn.classList.toggle('checked');
          on.classList.toggle('d-none');
          off.classList.toggle('d-none');
        })
      } else {
        return false;
      }
    });
  }
  