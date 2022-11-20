
package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour Step complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="Step"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="distance" type="{http://www.w3.org/2001/XMLSchema}double" minOccurs="0"/&gt;
 *         &lt;element name="duration" type="{http://www.w3.org/2001/XMLSchema}double" minOccurs="0"/&gt;
 *         &lt;element name="instruction" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="name" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="type" type="{http://www.w3.org/2001/XMLSchema}int" minOccurs="0"/&gt;
 *         &lt;element name="way_points" type="{http://schemas.microsoft.com/2003/10/Serialization/Arrays}ArrayOfint" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Step", propOrder = {
    "distance",
    "duration",
    "instruction",
    "name",
    "type",
    "wayPoints"
})
public class Step {

    protected Double distance;
    protected Double duration;
    @XmlElementRef(name = "instruction", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> instruction;
    @XmlElementRef(name = "name", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> name;
    protected Integer type;
    @XmlElementRef(name = "way_points", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<ArrayOfint> wayPoints;

    /**
     * Obtient la valeur de la propriété distance.
     * 
     * @return
     *     possible object is
     *     {@link Double }
     *     
     */
    public Double getDistance() {
        return distance;
    }

    /**
     * Définit la valeur de la propriété distance.
     * 
     * @param value
     *     allowed object is
     *     {@link Double }
     *     
     */
    public void setDistance(Double value) {
        this.distance = value;
    }

    /**
     * Obtient la valeur de la propriété duration.
     * 
     * @return
     *     possible object is
     *     {@link Double }
     *     
     */
    public Double getDuration() {
        return duration;
    }

    /**
     * Définit la valeur de la propriété duration.
     * 
     * @param value
     *     allowed object is
     *     {@link Double }
     *     
     */
    public void setDuration(Double value) {
        this.duration = value;
    }

    /**
     * Obtient la valeur de la propriété instruction.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getInstruction() {
        return instruction;
    }

    /**
     * Définit la valeur de la propriété instruction.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setInstruction(JAXBElement<String> value) {
        this.instruction = value;
    }

    /**
     * Obtient la valeur de la propriété name.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getName() {
        return name;
    }

    /**
     * Définit la valeur de la propriété name.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setName(JAXBElement<String> value) {
        this.name = value;
    }

    /**
     * Obtient la valeur de la propriété type.
     * 
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getType() {
        return type;
    }

    /**
     * Définit la valeur de la propriété type.
     * 
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setType(Integer value) {
        this.type = value;
    }

    /**
     * Obtient la valeur de la propriété wayPoints.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfint }{@code >}
     *     
     */
    public JAXBElement<ArrayOfint> getWayPoints() {
        return wayPoints;
    }

    /**
     * Définit la valeur de la propriété wayPoints.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfint }{@code >}
     *     
     */
    public void setWayPoints(JAXBElement<ArrayOfint> value) {
        this.wayPoints = value;
    }

}
