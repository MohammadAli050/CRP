function checkRequiredInputs(){
    $(".requiredField").validate({  
        rules:{
            txtFName:{required: true}
//            txtLName:{required: true},
//            txtAddress:{required: true},
//            txtPhone:{required: true}
        },
        messages:{
            txtFName:"*"
//            txtLName:"Required",
        //    txtAddress:"*",
        //    txtPhone:"*"
        }
    });
}   
    function checkValidInput()
    {
            $("#txtPhone").keydown(function(event) {
		    // Allow only backspace,delete,comma(,),left arrow,right arraow and Tab
		    if ( event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 188 
		        || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 9){
			    // let it happen, don't do anything
		    }
		    else {
			    // Ensure that it is a number and stop the keypress
			    if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode <96 ||event.keyCode > 105) ) {
				    event.preventDefault();	
			    }	
		    }
	    });
	    
	    $(".validateYearTextBox").keydown(function(event) {
                    // Allow only delete, backspace,left arrow,right arrow, Tab and numbers
                    if (!((event.keyCode == 46 || //delete
                        event.keyCode == 8  ||    //backspace
                        event.keyCode == 37 ||    //leftarow
                        event.keyCode == 39 ||    //rightarrow
                        event.keyCode == 9) ||    //rightarrow
                        $(this).val().length < 4 &&
                        ((event.keyCode >= 48 && event.keyCode <= 57) ||
                        (event.keyCode >= 96 && event.keyCode <= 105)))) {
                        // Stop the event
                        event.preventDefault();
                        return false;
                    }
                });
            
        
//            $(document).ready(function() {
//                checkValidInput();
//            });
        }
	   // }
//	    $(".validateYearTextBox").keydown(function(event)
//	        {
//	            // Allow only delete, backspace,left arrow,right arraow and Tab
//		        if ( 
//		               event.keyCode == 46 //delete
//		            || event.keyCode == 8  //backspace
//		            || event.keyCode == 37 //leftarow
//		            || event.keyCode == 39 //rightarrow
//		            || event.keyCode == 9  //tab
//		            )
//		            {
//			        // let it happen, don't do anything
//		            }
//		            else {
//			            // Ensure that it is a number and stop the keypress
//			            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode <96 ||event.keyCode > 105) ) {
//				            event.preventDefault();	
//			            }	
//		            }
//	        });
	        
//	       $(".validateYearTextBox").keyup(function(event)
//	            {
//                     var val = $(this).val();
//                     if (val.length > 4){
//                        alert ("Max length is 4");
//                        val = val.substring(0, valore.length - 1);
//                        $(this).val(val);
//                        $(this).focus();
//                        return false;
//                      }
//                });

    
//    $("#txtPhone").keydown(function(event) {
//		// Allow only backspace,delete,comma(,),left arrow,right arraow and Tab
//		if ( event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 188 
//		    || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 9){
//			// let it happen, don't do anything
//		}
//		else {
//			// Ensure that it is a number and stop the keypress
//			if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode <96 ||event.keyCode > 105) ) {
//				event.preventDefault();	
//			}	
//		}
//	});
	
//	$("").keydown(function(event)
//	        {
//	            // Allow only backspace,delete,comma(,),left arrow,right arraow and Tab
//		        if ( event.keyCode == 46 
//		            || event.keyCode == 8 
//		            || event.keyCode == 37 
//		            || event.keyCode == 39 
//		            || event.keyCode == 9)
//		            {
//			        // let it happen, don't do anything
//		            }
//		            else {
//			            // Ensure that it is a number and stop the keypress
//			            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode <96 ||event.keyCode > 105) ) {
//				            event.preventDefault();	
//			            }	
//		            }
//	});


