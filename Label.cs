class LabelAttribute : Attribute
{
 public string LabelText { get; set; }
 public LabelAttribute(string labelText)
 {
  LabelText = labelText;
 }
}


class Enterprise
{
 [Label("Название предприятия")]
 public string Name { get; set; }
 [Label("Имя директора")]
 public string DirectorName { get; set; }
 [Label("Регистрационный номер")]
 public string RegistrationNumber { get; set; }
}