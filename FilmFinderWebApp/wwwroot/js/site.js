// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Disable Button when no search text
$(document).ready(function () {
    $('Button[id="searchButton"]').attr('disabled', true);
    $('input[type="text"],textarea').on('keyup', function () {
        var text_value = $('input[name="searchString"]').val();
        if ( text_value != '') {
            $('Button[id="searchButton"]').attr('disabled', false);
        } else {
            $('Button[id="searchButton"]').attr('disabled', true);
        }
    });
});

//Filter Drop Down
        function btnToggle() { 
            document.getElementById("dropdown").classList.toggle("show"); 
        } 
          
        // Prevents menu from closing when clicked inside 
        document.getElementById("dropdown").addEventListener('click', function (event) { 
            event.stopPropagation(); 
        }); 
          
        // Closes the menu in the event of outside click 
        window.onclick = function(event) { 
            if (!event.target.matches('.dropdown-toggle')) { 
              
                var dropdowns =  
                document.getElementsByClassName("dropdown-menu"); 
                  
                var i; 
                for (i = 0; i < dropdowns.length; i++) { 
                    var openDropdown = dropdowns[i]; 
                    if (openDropdown.classList.contains('show')) { 
                        openDropdown.classList.remove('show'); 
                    } 
                } 
            } 
} 

//Checkbox
